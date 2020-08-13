using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RefreshInfoText : MonoBehaviour
{
    void Start()
    {
        GetComponent<TextMeshProUGUI>().text = "En Yüksek Skor\n" + PlayerPrefs.GetInt("HighScore", 0) + "\n\n" +
                                                "Oynanan Oyun\n" + PlayerPrefs.GetInt("PlayedGames", 0);
    }

    void Update()
    {
        
    }
}
