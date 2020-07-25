﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeTimeFreeze : MonoBehaviour
{
    public GameObject timeFreezeArea;
    public ParticleSystem timeFreeze;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("TimeFreeze"))
        {
            Instantiate(timeFreezeArea, Vector2.zero, Quaternion.identity);
            Instantiate(timeFreeze, Vector2.zero, Quaternion.identity);
            Destroy(collision.gameObject);
        }
    }
}
