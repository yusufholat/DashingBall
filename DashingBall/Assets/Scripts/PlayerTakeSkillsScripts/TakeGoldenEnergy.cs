using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeGoldenEnergy : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("GoldenEnergy"))
        {
            Destroy(collision.gameObject);
            EffectManager.PlayAnim("TakeGoldenEnergy");
        }
    }
}
