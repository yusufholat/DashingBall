using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHoleSpawner : MonoBehaviour
{
    public GameObject blackHole;
    public ParticleSystem infoEffect;
    private MeshCollider mesh;

    Vector2 whereToSpawn;
    float randX, randY;

    public float minSpawnRate;
    public float maxSpawnRate;

    float spawnTime;
    float nextSpawn = 0;

    void Start()
    {
        mesh = GetComponent<MeshCollider>();
        spawnTime = Random.Range(minSpawnRate, maxSpawnRate);
    }

    void Update()
    {
        if (GameManager.gameStarded == true)
            spawnBlackHole();
    }

    void spawnBlackHole()
    {
        nextSpawn += Time.deltaTime;
        if (nextSpawn > 3)
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
        randX = Random.Range(mesh.bounds.min.x, mesh.bounds.max.x);
        randY = Random.Range(mesh.bounds.min.y, mesh.bounds.max.y);
        whereToSpawn = new Vector2(randX, randY);
        Instantiate(infoEffect, whereToSpawn, Quaternion.identity);
        yield return new WaitForSeconds(2f);
        Instantiate(blackHole, whereToSpawn, Quaternion.identity);
    }
}
