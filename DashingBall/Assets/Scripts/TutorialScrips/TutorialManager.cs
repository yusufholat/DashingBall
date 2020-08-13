using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialManager : MonoBehaviour
{
    public GameObject tutorialPlayerTemplate;
    GameObject tutorialPlayerInstantiated;

    public static bool tutorialStarted = false;
    public static bool energySystemOn = false;
    public static bool scoreSystemOn = false;


    public GameObject startPopup;
    public GameObject playerControllerPopup;
    public GameObject waitPlayerControllerPopup;
    public GameObject skorSystemPopup;
    public GameObject wait15ScorePopup;
    public GameObject energySystemPopup;
    public GameObject waitEnergySystemPopup;
    public GameObject enemySystemPopup;
    public GameObject itemSystemPopup;

    public static bool silentMode = false;

    public GameObject energyInfo;
    public GameObject antiEnergyInfo;
    public GameObject GoldenEnergyInfo;
    public GameObject blackHoleInfo;
    public GameObject timeFreezeInfo;
    public GameObject ShieldInfo;

    public GameObject itemSystemLastInfo;
    public GameObject gotoMenuInfoPopup;


    public Animator scenetransition;

    public GameObject scoreText;
    public GameObject energyPanel;
    public GameObject enemySpawner;

    public static int playerTouchCount;
    public static bool playerControlBall = false;

    public static bool playerControlScore = false;

    public static TutorialManager instance;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        tutorialPlayerInstantiated = Instantiate(tutorialPlayerTemplate, new Vector2(0, -4), Quaternion.identity);
        energySystemOn = false;
        scoreSystemOn = false;
        tutorialStarted = false;
    }

    public void RestartPlayer()
    {
        Destroy(tutorialPlayerInstantiated);
        tutorialPlayerInstantiated = Instantiate(tutorialPlayerTemplate, new Vector2(0, -4), Quaternion.identity);
    }


    public void ShowPlayerControllerPopup()
    {
        PlayerPrefs.SetInt("ComplateTutorial", 1);
        startPopup.GetComponent<Animator>().SetTrigger("closepopup");
        playerControllerPopup.SetActive(true);
    }

    public void WaitPlayerController()
    {
        playerControllerPopup.GetComponent<Animator>().SetTrigger("closepopup");
        waitPlayerControllerPopup.SetActive(true);
        tutorialStarted = true;
        playerControlBall = true;
    }

    public void ShowSkorSystemPopup()
    {
        waitPlayerControllerPopup.GetComponent<Animator>().SetTrigger("closepopup");
        skorSystemPopup.SetActive(true);
    }

    public void WaitScoreSystem()
    {
        scoreText.SetActive(true);
        skorSystemPopup.GetComponent<Animator>().SetTrigger("closepopup");
        wait15ScorePopup.SetActive(true);
        scoreSystemOn = true;
        playerControlScore = true;
    }

    public void ShowEnergySystemPopup()
    {
        wait15ScorePopup.GetComponent<Animator>().SetTrigger("closepopup");
        energySystemPopup.SetActive(true);
    }

    public void ShowEnergyPanel()
    {
        energyPanel.SetActive(true);
        energySystemPopup.GetComponent<Animator>().SetTrigger("closepopup");
        waitEnergySystemPopup.SetActive(true);
        energySystemOn = true;
        StartCoroutine(WaitEnergyPanel());
    }
    IEnumerator WaitEnergyPanel()
    {
        yield return new WaitForSeconds(10f);
        waitEnergySystemPopup.GetComponent<Animator>().SetTrigger("closepopup");
        enemySystemPopup.SetActive(true);
    }

    public void waitEnemySystem()
    {
        enemySpawner.SetActive(true);
        enemySystemPopup.GetComponent<Animator>().SetTrigger("closepopup");       
        StartCoroutine(waitEnemyPanel());
    }
    IEnumerator waitEnemyPanel()
    {
        yield return new WaitForSeconds(15f);
        itemSystemPopup.SetActive(true);
        silentMode = true;
        Time.timeScale = 0.25f;
    }

    public void ShowEnergyInfo()
    {
        itemSystemPopup.GetComponent<Animator>().SetTrigger("closepopup");
        energyInfo.SetActive(true);
    }

    public void ShowAntiEnergyInfo()
    {
        energyInfo.GetComponent<Animator>().SetTrigger("closepopup");
        antiEnergyInfo.SetActive(true);
    }

    public void ShowGoldenEnergyInfo()
    {
        antiEnergyInfo.GetComponent<Animator>().SetTrigger("closepopup");
        GoldenEnergyInfo.SetActive(true);
    }

    public void ShowBlackHoleInfo()
    {
        GoldenEnergyInfo.GetComponent<Animator>().SetTrigger("closepopup");
        blackHoleInfo.SetActive(true);
    }

    public void ShowTimeFreezeInfo()
    {
        blackHoleInfo.GetComponent<Animator>().SetTrigger("closepopup");
        timeFreezeInfo.SetActive(true);
    }

    public void ShowShieldInfo()
    {
        timeFreezeInfo.GetComponent<Animator>().SetTrigger("closepopup");
        ShieldInfo.SetActive(true);
    }

    public void ShowLastInfo()
    {
        ShieldInfo.GetComponent<Animator>().SetTrigger("closepopup");
        itemSystemLastInfo.SetActive(true);
    }

    public void ShowGoToGameInfo()
    {
        itemSystemLastInfo.GetComponent<Animator>().SetTrigger("closepopup");
        gotoMenuInfoPopup.SetActive(true);
    }

    public void gotoGame()
    {
        gotoMenuInfoPopup.GetComponent<Animator>().SetTrigger("closepopup");
        StartCoroutine(LoadGoToGame());
    }
    IEnumerator LoadGoToGame()
    {
        Time.timeScale = 1f;
        scenetransition.SetTrigger("tutorialend");
        yield return new WaitForSeconds(1f);
        GameManager.MenuMusicPlaying = false;
        GameManager.gameStarded = true;
        GameManager.tutorialPlayerInstantiete = false;
        SceneManager.LoadScene("Game");
    }

    public void goToMenu()
    {
        PlayerPrefs.SetInt("ComplateTutorial", 1);
        startPopup.GetComponent<Animator>().SetTrigger("closepopup");
        StartCoroutine(LoadGoToMenu());
    }
    IEnumerator LoadGoToMenu()
    {
        scenetransition.SetTrigger("tutorialquit");
        yield return new WaitForSeconds(1f);
        GameManager.tutorialPlayerInstantiete = false;
        GameManager.MenuMusicPlaying = false;
        SceneManager.LoadScene("Menu");
    }
}
