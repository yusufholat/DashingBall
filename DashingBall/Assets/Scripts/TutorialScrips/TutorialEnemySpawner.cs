using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialEnemySpawner : MonoBehaviour
{
    public GameObject enemy;
    Vector2 whereToSpawn;
    float randX;

    public static float spawnRate = 2f;
    float nextSpawn = 0;

    void Update()
    {
        if (!PlayerManager.timeFreezePower)
            Spawn();
    }

    void Spawn()
    {
        nextSpawn += Time.deltaTime;
        if (nextSpawn > spawnRate)
        {
            randX = Random.Range(-4.5f, 4.5f);
            whereToSpawn = new Vector2(randX, transform.position.y);
            Instantiate(enemy, whereToSpawn, Quaternion.identity);
            nextSpawn = 0;
        }
    }
}
