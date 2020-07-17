using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashHealth : MonoBehaviour
{
    float rotZ;
    public float speed = 10f;

    void Update()
    {
        rotZ += Time.deltaTime * speed;
        transform.rotation = Quaternion.Euler(0, 0, rotZ);
    }
}
