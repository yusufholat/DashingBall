using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSpawner : MonoBehaviour
{
    public GameObject health;
    private MeshCollider mesh;

    Vector2 whereToSpawn;
    float randX, randY;

    public float spawnRate;
    float nextSpawn = 0;

    void Start()
    {
        mesh = GetComponent<MeshCollider>();
    }

    void Update()
    {
        if (UIManager.gameIsStarded == true)
            spawnHealth();
    }

    void spawnHealth()
    {
        nextSpawn += Time.deltaTime;
        if (nextSpawn > spawnRate)
        {
            randX = Random.Range(mesh.bounds.min.x, mesh.bounds.max.x);
            randY = Random.Range(mesh.bounds.min.y, mesh.bounds.max.y);
            whereToSpawn = new Vector2(randX, randY);
            Instantiate(health, whereToSpawn, Quaternion.identity);

            nextSpawn = 0;
        }       
    }
}
