using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemy;
    Vector2 whereToSpawn;
    float randX;

    public float spawnRate;
    float nextSpawn = 0;

    void Update()
    {
        if(UIManager.gameIsStarded == true)
        Spawn();
    }

    void Spawn() {
        nextSpawn += Time.deltaTime;
        if(nextSpawn > spawnRate)
        {
            randX = Random.Range(-4.5f, 4.5f);
            whereToSpawn = new Vector2(randX, transform.position.y);
            Instantiate(enemy, whereToSpawn, Quaternion.identity);
            nextSpawn = 0;
        }
    }
}
