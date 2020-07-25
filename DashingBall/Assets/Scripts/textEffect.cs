using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class textEffect : MonoBehaviour
{
    private TextMeshProUGUI text;
    float lerpTime = 3.5f;
    bool dondur;
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        //if(text.color == Color.white)
        //    dondur = true;

        //if(dondur == true)
        //text.color = Color.Lerp(text.color, Color.black, lerpTime * Time.deltaTime);

        //if (text.color == Color.black)
        //    dondur = false;

        //if (dondur == false)
        //    text.color = Color.Lerp(text.color, Color.white, lerpTime * Time.deltaTime);
    }
}
