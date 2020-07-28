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

    public TextMeshProUGUI countEnergyText;
    public TextMeshProUGUI countAntiEnergyText;
    public TextMeshProUGUI countGoldenEnergyText;
    public TextMeshProUGUI countBlackHoleText;
    public TextMeshProUGUI countTimeFreezeText;
    public TextMeshProUGUI countShieldText;

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

            countEnergyText.text = "x" + PlayerManager.countEnergy.ToString();
            countAntiEnergyText.text = "x" + PlayerManager.countAntiEnergy.ToString();
            countGoldenEnergyText.text = "x" + PlayerManager.countGoldenEnergy.ToString();
            countBlackHoleText.text = "x" + PlayerManager.countBlackHole.ToString();
            countTimeFreezeText.text = "x" + PlayerManager.countTimeFreeze.ToString();
            countShieldText.text = "x" + PlayerManager.countShield.ToString();

            PlayerPrefs.SetInt("energy", PlayerManager.countEnergy + PlayerPrefs.GetInt("energy", 0));
            PlayerPrefs.SetInt("antienergy", PlayerManager.countAntiEnergy + PlayerPrefs.GetInt("antienergy", 0));
            PlayerPrefs.SetInt("goldenenergy", PlayerManager.countGoldenEnergy + PlayerPrefs.GetInt("goldenenergy", 0));
            PlayerPrefs.SetInt("blackhole", PlayerManager.countBlackHole + PlayerPrefs.GetInt("blackhole", 0));
            PlayerPrefs.SetInt("timefreeze", PlayerManager.countTimeFreeze + PlayerPrefs.GetInt("timefreeze",0));
            PlayerPrefs.SetInt("shield", PlayerManager.countShield + PlayerPrefs.GetInt("shield", 0));

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
