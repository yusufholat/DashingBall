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

    public static bool musicOn;
    public static bool vibrationOn = true;
    public static bool MenuMusicPlaying;

    int nextTime = 0, rate = 30;
    bool endLevel = false;
    public static int gameDifficulty = 1;

    public Color[] collors;

    public Color menuColor;

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
        MenuMusicPlaying = false;
        Camera.main.backgroundColor = menuColor;
    }

    private void Update()
    {
        if(musicOn == true && gameStarded == false && MenuMusicPlaying == false)
        {
            FindObjectOfType<AudioManager>().Play("MenuMusic");
            MenuMusicPlaying = true;
        }
        if (gameStarded)
        {
            Debug.Log(Time.timeSinceLevelLoad);
            if (Time.timeSinceLevelLoad > nextTime)
            {
                if (!endLevel)
                {
                    setGameDifficulty(Time.timeSinceLevelLoad);
                    nextTime += rate;
                }
            }
            
        }
        else nextTime = 0;


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
        Time.timeScale = 0.25f;
        gameOver = false;

        int highScore = PlayerPrefs.GetInt("HighScore", 0);

        if (PlayerManager.score > highScore)
            PlayerPrefs.SetInt("HighScore", PlayerManager.score);

        PlayerManager.totalCoin = PlayerManager.score + PlayerPrefs.GetInt("TotalCoin", 3000);

        PlayerPrefs.SetInt("TotalCoin", PlayerManager.score + PlayerPrefs.GetInt("TotalCoin", 3000));
        PlayerPrefs.SetInt("energy", PlayerManager.countEnergy + PlayerPrefs.GetInt("energy", 0));
        PlayerPrefs.SetInt("antienergy", PlayerManager.countAntiEnergy + PlayerPrefs.GetInt("antienergy", 0));
        PlayerPrefs.SetInt("goldenenergy", PlayerManager.countGoldenEnergy + PlayerPrefs.GetInt("goldenenergy", 0));
        PlayerPrefs.SetInt("blackhole", PlayerManager.countBlackHole + PlayerPrefs.GetInt("blackhole", 0));
        PlayerPrefs.SetInt("timefreeze", PlayerManager.countTimeFreeze + PlayerPrefs.GetInt("timefreeze", 0));
        PlayerPrefs.SetInt("shield", PlayerManager.countShield + PlayerPrefs.GetInt("shield", 0));

    }

    public void restartGame()
    {
        Time.timeScale = 1f;
        gameOver = false;
        nextTime = 0;
        SceneManager.LoadScene("Game");
        Camera.main.backgroundColor = menuColor;
    }

    public void goToMenu()
    {
        Time.timeScale = 1f;
        gameStarded = false;
        gameOver = false;
        SceneManager.LoadScene("Menu");
        Camera.main.backgroundColor = menuColor;
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
            EnemySpawner.spawnRate = 1f;
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

}
