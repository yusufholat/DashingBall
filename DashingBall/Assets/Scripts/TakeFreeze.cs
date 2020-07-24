using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeFreeze : MonoBehaviour
{
    public ParticleSystem effect;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if (collision.gameObject.CompareTag("BlackHole"))
        //{
        //    Destroy(collision.gameObject);
        //    Instantiate(effect, collision.transform.position, Quaternion.identity);
        //}
    }
}
