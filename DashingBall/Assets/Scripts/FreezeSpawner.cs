using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeSpawner : MonoBehaviour
{
    public GameObject freeze;
    private MeshCollider mesh;

    Vector2 whereToSpawn;
    float randX, randY;

    public float spawnRate;
    float nextSpawn = 0;

    void Start()
    {
        mesh = GetComponent<MeshCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.gameStarded == true)
            spawnFreeze();
    }

    void spawnFreeze()
    {
        nextSpawn += Time.deltaTime;
        if (nextSpawn > spawnRate)
        {
            randX = Random.Range(mesh.bounds.min.x, mesh.bounds.max.x);
            randY = Random.Range(mesh.bounds.min.y, mesh.bounds.max.y);
            whereToSpawn = new Vector2(randX, randY);
            Instantiate(freeze, whereToSpawn, Quaternion.identity);

            nextSpawn = 0;
        }
    }
}
