using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static bool gameIsStarded = true;
    public static bool gameIsPaused = false;
    public static bool gameIsOver = false;

    public GameObject pauseMenuUI;
    public GameObject energyBarUI;
    public GameObject pauseButtonUI;
    public GameObject scoreTextUI;
    public GameObject gameOverUI;

    public GameObject player;

    public void Resume()
    {
        if(gameIsPaused == true)
        {
            Time.timeScale = 1f;
            pauseMenuUI.SetActive(false);
            pauseButtonUI.SetActive(true);
            gameIsPaused = false;
        }
    }

    public void pauseGame()
    {
        Time.timeScale = 0f;
        pauseMenuUI.SetActive(true);
        pauseButtonUI.SetActive(false);
        gameIsPaused = true;
    }

    public void restartGame()
    {
        Time.timeScale = 1f;
        gameIsOver = false;
        SceneManager.LoadScene("Game");
    }

    private void Update()
    {
        if(gameIsOver == true)
        {
            Time.timeScale = 0.25f;
            gameOverUI.SetActive(true);
            pauseButtonUI.SetActive(false);
        }
    }

    public void goToMenu()
    {
        gameIsOver = false;
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }
}
