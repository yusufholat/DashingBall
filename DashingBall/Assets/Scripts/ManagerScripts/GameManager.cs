using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static bool gameStarded;
    public static bool gameOver = false;

    //public static bool tutorialStarted;

    public static bool musicOn;
    public static bool vibrationOn = true;

    int nextTime = 0, rate = 30;
    float gametime = 0;
    bool endLevel = false;
    public static int gameDifficulty = 1;

    public Color[] collors;

    public Color menuColor;

    public static bool tutorialPlayerInstantiete = true;

    public static int defaultItemCounts = 10;
    public static int defaultTotalCoin = 1000;

    public static GameManager instance;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        gameStarded = false;
        musicOn = (PlayerPrefs.GetInt("MusicOn", 1) != 0);

        if ((PlayerPrefs.GetInt("ComplateTutorial", 0) == 1))
        {
            tutorialPlayerInstantiete = false;
            SceneManager.LoadScene("Menu");
            switchMenuMusic();
        }

        Camera.main.backgroundColor = menuColor;
    }

    public void switchMenuMusic() {
        if (musicOn == true && gameStarded == false)
        {
            FindObjectOfType<AudioManager>().Play("MenuMusic");
        }
        else FindObjectOfType<AudioManager>().Stop("MenuMusic");
    }

    private void Update()
    {
        if (gameStarded)
        {
            gametime += Time.deltaTime;
            if (gametime > nextTime)
            {
                if (!endLevel)
                {
                    if(!gameOver)
                    setGameDifficulty(gametime);
                    nextTime += rate;
                }
            }
        }        

    }


    public static bool IsPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }


    public void GameOver()
    {
        gameOver = true;
        Time.timeScale = 0.25f;
        FindObjectOfType<AudioManager>().Play("GameOver");
        if (vibrationOn) Handheld.Vibrate();

        SetPlayerPrefs();
        UIManager.instance.ShowGameOver();
        

        ItemManager.instance.RefreshList();
    }

    public void restartGame()
    {
        ResetItemCounts();
        resetGame();
        SceneManager.LoadScene("Game");       
    }



    public void goToMenu()
    {
        ResetItemCounts();
        resetGame();
        gameStarded = false;
        switchMenuMusic();
        SceneManager.LoadScene("Menu");
    }



    public void setGameDifficulty(float gameTime)
    {
        if (gameTime > 120)
        {
            AudioManager.instance.Play("LevelUpEnd");
            EnemySpawner.spawnRate = 0.7f;
            gameDifficulty = 5;
            Camera.main.backgroundColor = collors[4];
            endLevel = true;
        }
        else if (gameTime > 90)
        {
            AudioManager.instance.Play("LevelUp");
            EnemySpawner.spawnRate = 1.1f;
            gameDifficulty = 4;
            Camera.main.backgroundColor = collors[3];
        }
        else if (gameTime > 60)
        {
            AudioManager.instance.Play("LevelUp");
            EnemySpawner.spawnRate = 1.5f;
            gameDifficulty = 3;
            Camera.main.backgroundColor = collors[2];
        }
        else if (gameTime > 30)
        {
            EnemySpawner.spawnRate = 2f;
            gameDifficulty = 2;
            Camera.main.backgroundColor = collors[1];
            AudioManager.instance.Play("LevelUp");
        }
        else if (gameTime > 0){
            EnemySpawner.spawnRate = 2.5f;
            gameDifficulty = 1;
            Camera.main.backgroundColor = collors[0];
            endLevel = false;
        }
    }



    public void ResetItemCounts()
    {
        PlayerManager.score = 0;
        PlayerManager.countEnergy = 0;
        PlayerManager.countAntiEnergy = 0;
        PlayerManager.countGoldenEnergy = 0;
        PlayerManager.countBlackHole = 0;
        PlayerManager.countTimeFreeze = 0;
        PlayerManager.countShield = 0;
    }



    void SetPlayerPrefs()
    {
        PlayerPrefs.SetInt("PlayedGames", PlayerPrefs.GetInt("PlayedGames", 0) + 1);

        if (PlayerManager.score > PlayerPrefs.GetInt("HighScore", 0))
        {
            PlayerPrefs.SetInt("HighScore", PlayerManager.score);
            PlayerGPSManager.instance.UpdateLeaderBoardScore(PlayerManager.score);
        }

        if (PlayerManager.score + PlayerPrefs.GetInt("TotalCoin", defaultTotalCoin) > 9999)
            PlayerPrefs.SetInt("TotalCoin", 9999);
        else PlayerPrefs.SetInt("TotalCoin", PlayerManager.score + PlayerPrefs.GetInt("TotalCoin", defaultTotalCoin));

        if (PlayerManager.countEnergy + PlayerPrefs.GetInt("energy", defaultItemCounts) > 999)
            PlayerPrefs.SetInt("energy", 999);
        else PlayerPrefs.SetInt("energy", PlayerManager.countEnergy + PlayerPrefs.GetInt("energy", defaultItemCounts));

        if (PlayerManager.countAntiEnergy + PlayerPrefs.GetInt("antienergy", defaultItemCounts) > 999)
            PlayerPrefs.SetInt("antienergy", 999);
        else PlayerPrefs.SetInt("antienergy", PlayerManager.countAntiEnergy + PlayerPrefs.GetInt("antienergy", defaultItemCounts));

        if (PlayerManager.countGoldenEnergy + PlayerPrefs.GetInt("goldenenergy", defaultItemCounts) > 999)
            PlayerPrefs.SetInt("goldenenergy", 999);
        else PlayerPrefs.SetInt("goldenenergy", PlayerManager.countGoldenEnergy + PlayerPrefs.GetInt("goldenenergy", defaultItemCounts));

        if (PlayerManager.countBlackHole + PlayerPrefs.GetInt("blackhole", defaultItemCounts) > 999)
            PlayerPrefs.SetInt("blackhole", 999);
        else PlayerPrefs.SetInt("blackhole", PlayerManager.countBlackHole + PlayerPrefs.GetInt("blackhole", defaultItemCounts));

        if (PlayerManager.countTimeFreeze + PlayerPrefs.GetInt("timefreeze", defaultItemCounts) > 999)
            PlayerPrefs.SetInt("timefreeze", 999);
        else PlayerPrefs.SetInt("timefreeze", PlayerManager.countTimeFreeze + PlayerPrefs.GetInt("timefreeze", defaultItemCounts));

        if (PlayerManager.countShield + PlayerPrefs.GetInt("shield", defaultItemCounts) > 999)
            PlayerPrefs.SetInt("shield", 999);
        else PlayerPrefs.SetInt("shield", PlayerManager.countShield + PlayerPrefs.GetInt("shield", defaultItemCounts));

    }

    void resetGame()
    {
        Time.timeScale = 1f;
        gameOver = false;
        nextTime = 0;
        gametime = 0;
        gameDifficulty = 1;
        EnemySpawner.spawnRate = 2.5f;
        PlayerManager.goldenEnergyPower = false;
        PlayerManager.timeFreezePower = false;
        endLevel = false;
        Camera.main.backgroundColor = menuColor;
    }

}
