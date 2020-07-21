using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public GameObject energyBarUI;
    public GameObject pauseMenuUI; 
    public GameObject pauseButtonUI;
    public GameObject scoreTextUI;
    public GameObject gameOverMenu;

    public GameObject player;

    public void Resume()
    {
        if(GameManager.gamePaused == true)
        {
            Time.timeScale = 1f;
            pauseMenuUI.SetActive(false);
            pauseButtonUI.SetActive(true);
            GameManager.gamePaused = false;
        }
    }

    public void Pause()
    {
        Time.timeScale = 0f;
        pauseMenuUI.SetActive(true);
        //pauseButtonUI.SetActive(false);
        GameManager.gamePaused = true;
    }

    public void restartGame()
    {
        Time.timeScale = 1f;
        GameManager.gameOver = false;
        SceneManager.LoadScene("Game");
    }

    private void Update()
    {
        if(GameManager.gameOver == true)
        {
            //if(GameManager.vibrationOn)
            //Handheld.Vibrate();

            Time.timeScale = 0.1f;
            int newcoin = PlayerManager.score + PlayerPrefs.GetInt("TotalCoin" , 1000);

            PlayerPrefs.SetInt("TotalCoin", newcoin);
            PlayerManager.totalCoin = newcoin;

            gameOverMenu.SetActive(true);
            pauseButtonUI.SetActive(false);

            GameManager.gameOver = false;
        }
    }

    public void goToMenu()
    {
        GameManager.gameStarded = false;
        GameManager.gameOver = false;
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }
}
