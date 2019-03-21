using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Transform[] flyerSpawner;
    public Transform[] patrollerSpawner;

    public GameObject flyerEnemy;
    public GameObject[] patrollerEnemy;

    public float flyerTimer;
    public float patrollerTimer;

    public float enemyMax = 5;

    public float enemyCount;

    private void Start()
    {
        flyerTimer = 3;
        patrollerTimer = 3;
    }

    private void Update()
    {
        EnemyCounter();
        SpawnPatrollerEnemies();
        SpawnFlyerEnemies();
    }

    void EnemyCounter()
    {
        enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
    }

    void SpawnPatrollerEnemies()
    {
        if(patrollerTimer <= 0 && enemyCount < enemyMax)
        {
            Transform spawner = patrollerSpawner[Random.Range(0, patrollerSpawner.Length)];
            int randomIndex = Random.Range(0, patrollerEnemy.Length);
            GameObject clone = Instantiate(patrollerEnemy[randomIndex], spawner.position, Quaternion.identity);            
            clone.GetComponent<Enemy>().SetOrigin(spawner);
            Debug.Log(clone.name + " appear");
            patrollerTimer = Random.Range(2f, 3f);

        }
        else
        {
            patrollerTimer -= Time.deltaTime;
        }
    }

    void SpawnFlyerEnemies()
    {
        if(flyerTimer <= 0 && enemyCount < enemyMax)
        {
            Transform spawner = flyerSpawner[Random.Range(0, flyerSpawner.Length)];
            GameObject clone = Instantiate(flyerEnemy, spawner.position, Quaternion.identity);
            if(spawner.position.x == 24) {
                clone.transform.Rotate(0f, 180f, 0f);
            }
            clone.GetComponent<Enemy>().SetOrigin(spawner);
            Debug.Log(clone.name + " appear");
            flyerTimer = Random.Range(2f, 3f);
        }
        else
        {
            flyerTimer -= Time.deltaTime;
        }
    }
}
