using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HealthController : MonoBehaviour
{
    public float maxHealth;
    public float currentHealth;
    public float damageTimer;
    public float maxTimer;

    void Start()
    {
        this.damageTimer = 0;
        this.currentHealth = this.maxHealth;
    }

    private void Update()
    {
        damageTimer -= Time.deltaTime;
    }

    public void TakeDamage(int damage, out bool damageTaken)
    {
        if(damageTimer <= 0)
        {
            this.currentHealth -= damage;
            this.damageTimer = maxTimer;
            damageTaken = true;
        } else
        {
            damageTaken = false;
            Debug.Log("Invincible Time");
        }

        if(this.currentHealth <= 0)
        {
            this.currentHealth = 0;
            this.Die();
        }
    }
    public void Die()
    {
        Destroy(gameObject);
        if (gameObject.CompareTag("Player"))
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
