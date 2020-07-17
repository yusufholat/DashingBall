using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeHealth : MonoBehaviour
{
    public ParticleSystem lastHealthEffect;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Health"))
        {
            Destroy(collision.gameObject);
            Instantiate(lastHealthEffect, collision.transform.position, Quaternion.identity);
        }
    }
}
