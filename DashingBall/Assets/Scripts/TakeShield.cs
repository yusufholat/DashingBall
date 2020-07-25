using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeShield : MonoBehaviour
{
    public GameObject shieldArea;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Shield"))
        {
            GameObject shld = Instantiate(shieldArea, transform.position, Quaternion.identity);
            shld.transform.parent = gameObject.transform;
            shld.GetComponent<Animator>().Rebind();
            Destroy(collision.gameObject);
        }
    }
}
