using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
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

            GameManager.instance.ResetItemCounts();
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
