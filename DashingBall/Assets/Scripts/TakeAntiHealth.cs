using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeAntiHealth : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("AntiHealth"))
        {
            Destroy(collision.gameObject);
            EffectManager.PlayAnim("TakeAntiEnergy");
            PlayerManager.countAntiEnergy++;
        }
    }
}
