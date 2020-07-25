using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public GameObject energyBarUI;
    public GameObject pauseMenuUI; 
    public GameObject pauseButtonUI;
    public GameObject scoreTextUI;
    public GameObject gameOverMenu;

    public TextMeshProUGUI gameOverHighScoreText;
    public TextMeshProUGUI gameOverScoreText;

    Animator transition;

    //public void Resume()
    //{
    //    Time.timeScale = 1f;
    //    pauseMenuUI.SetActive(false);
    //    pauseButtonUI.SetActive(true);

    //}

    //public void Pause()
    //{
    //    Time.timeScale = 0f;
    //    pauseMenuUI.SetActive(true);
    //}

    private void Start()
    {
        transition = GetComponent<Animator>();    
    }

    public void restartGame()
    {
        StartCoroutine(Restart());
    }

    IEnumerator Restart()
    {
        Time.timeScale = 1f;
        transition.SetTrigger("restart");
        yield return new WaitForSeconds(1f);
        GameManager.instance.restartGame();
    }

    private void Update()
    {
        if(GameManager.gameOver == true)
        {
            GameManager.instance.GameOver();

            gameOverHighScoreText.text = PlayerPrefs.GetInt("HighScore", 0).ToString();
            gameOverScoreText.text = PlayerManager.score.ToString();

            scoreTextUI.SetActive(false);
            gameOverMenu.SetActive(true);
        }
    }

    public void goToMenu()
    {
        StartCoroutine(GoToMenu());        
    }

    IEnumerator GoToMenu()
    {
        Time.timeScale = 1f;
        transition.SetTrigger("restart");
        yield return new WaitForSeconds(1f); 
        GameManager.instance.goToMenu();
    }
}
