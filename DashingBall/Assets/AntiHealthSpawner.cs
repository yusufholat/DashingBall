using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntiHealthSpawner : MonoBehaviour
{
    public GameObject antiHealth;
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
        if (GameManager.gameStarded == true)
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
            Instantiate(antiHealth, whereToSpawn, Quaternion.identity);

            nextSpawn = 0;
        }
    }
}
