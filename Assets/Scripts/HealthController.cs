using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour
{
    public float maxHealth;
    public float currentHealth;
    void Start()
    {
        this.currentHealth = this.maxHealth;
    }
    public void TakeDamage(int damage)
    {
        this.currentHealth -= damage;

        if(this.currentHealth <= 0)
        {
            this.currentHealth = 0;
            this.Die();
        }
    }
    public void Die()
    {
        Destroy(gameObject);
    }
}
