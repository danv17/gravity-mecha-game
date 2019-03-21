using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByExit : MonoBehaviour
{
    private void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log(other.name);
        if(other.CompareTag("Enemy"))
            Destroy(other.gameObject);
    }
}
