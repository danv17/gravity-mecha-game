using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot : MonoBehaviour
{
    public int damage;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            return;

        if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<EnemyController>().healthController.TakeDamage(damage);
        }
        Destroy(gameObject);
    }
}
