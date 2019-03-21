using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot : MonoBehaviour
{
    public int damage;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Finish"))
            return;

        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<Enemy>().healthController.TakeDamage(damage);
            Debug.Log(other.name + " had taken damage");
        }

        Destroy(gameObject);
    }
}
