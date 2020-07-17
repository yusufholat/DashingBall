using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menuUIManager : MonoBehaviour
{

    public void playGame()
    {
        SceneManager.LoadScene("Scene");
    }
}
