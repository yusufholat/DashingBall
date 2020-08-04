using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeBlackHole : MonoBehaviour
{
    public GameObject blackHoleArea;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("BlackHole"))
        {
            Instantiate(blackHoleArea, collision.transform.position, Quaternion.identity);
            Destroy(collision.gameObject);
        }
    }
}
