using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetShopEdges : MonoBehaviour
{
    public EdgeCollider2D playerArea;
    void Start()
    {
        playerArea = GetComponent<EdgeCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
