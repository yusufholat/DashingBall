using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemy;
    Vector2 whereToSpawn;
    float randX;
    float minX, maxX, offset = 1.5f;

    public static float spawnRate = 2f;
    float nextSpawn = 0;

    private void Awake()
    {
        var lowerLeft = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0));
        var upperRight = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        minX = lowerLeft.x;
        maxX = upperRight.x;
    }

    void Update()
    {
        if(GameManager.gameStarded && !PlayerManager.timeFreezePower)
        Spawn();
    }

    void Spawn() {
        nextSpawn += Time.deltaTime;
        if(nextSpawn > spawnRate)
        {
            randX = Random.Range(minX + offset, 4.5f - offset);
            whereToSpawn = new Vector2(randX, transform.position.y);
            Instantiate(enemy, whereToSpawn, Quaternion.identity);
            nextSpawn = 0;
        }
    }
}
