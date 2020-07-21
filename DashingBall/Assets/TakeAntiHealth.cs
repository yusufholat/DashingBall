using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeAntiHealth : MonoBehaviour
{
    public ParticleSystem lastHealthEffect;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("AntiHealth"))
        {
            Destroy(collision.gameObject);
            Instantiate(lastHealthEffect, collision.transform.position, Quaternion.identity);
        }
    }
}
