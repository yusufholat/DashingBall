using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergySpawner : MonoBehaviour
{
    public GameObject energy;
    public ParticleSystem infoEffect;

    Vector2 whereToSpawn;
    float randX, randY;

    public float minSpawnRate;
    public float maxSpawnRate;

    float spawnTime;
    float nextSpawn = 0;

    float minX, maxX, minY, maxY, offset = 1.5f, offsetTop = 5f;

    private void Awake()
    {
        var lowerLeft = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0));
        var upperRight = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        minX = lowerLeft.x;
        maxX = upperRight.x;
        maxY = upperRight.y;
        minY = lowerLeft.y;
    }

    void Start()
    {
        spawnTime = Random.Range(minSpawnRate, maxSpawnRate);
    }

    void Update()
    {
        if (GameManager.gameStarded == true && GameManager.gameDifficulty >= 1)
            spawnHealth();
    }

    void spawnHealth()
    {
        nextSpawn += Time.deltaTime;
        if(nextSpawn > 3)
            infoEffect.Play();
        if (nextSpawn > spawnTime)
        {
            StartCoroutine(Spawn());
            spawnTime = Random.Range(minSpawnRate, maxSpawnRate);
            nextSpawn = 0;
        }
    }

    IEnumerator Spawn()
    {
        randX = Random.Range(minX + offset, maxX - offset);
        randY = Random.Range(minY + offset, maxY - offsetTop);
        whereToSpawn = new Vector2(randX, randY);
        Instantiate(infoEffect, whereToSpawn, Quaternion.identity);
        yield return new WaitForSeconds(2f);
        Instantiate(energy, whereToSpawn, Quaternion.identity);
    }
}
