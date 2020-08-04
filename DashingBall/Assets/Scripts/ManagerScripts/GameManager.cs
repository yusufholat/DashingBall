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
    float gametime = 0;
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
            gametime += Time.deltaTime;
            if (gametime > nextTime)
            {
                if (!endLevel)
                {
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
        Time.timeScale = 0.25f;
        gameOver = false;

        if (vibrationOn) Handheld.Vibrate();

        FindObjectOfType<AudioManager>().Play("GameOver");
        SetPlayerPrefs();
    }



    public void restartGame()
    {
        resetGame();
        SceneManager.LoadScene("Game");       
    }



    public void goToMenu()
    {
        resetGame();
        gameStarded = false;
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



    void SetPlayerPrefs()
    {
        if (PlayerManager.score > PlayerPrefs.GetInt("HighScore", 0))
            PlayerPrefs.SetInt("HighScore", PlayerManager.score);

        if (PlayerManager.score + PlayerPrefs.GetInt("TotalCoin", 1000) > 9999)
            PlayerPrefs.SetInt("TotalCoin", 999);
        else PlayerPrefs.SetInt("TotalCoin", PlayerManager.score + PlayerPrefs.GetInt("TotalCoin", 3000));

        if (PlayerManager.countEnergy + PlayerPrefs.GetInt("energy", 0) > 999)
            PlayerPrefs.SetInt("energy", 999);
        else PlayerPrefs.SetInt("energy", PlayerManager.countEnergy + PlayerPrefs.GetInt("energy", 50));

        if (PlayerManager.countAntiEnergy + PlayerPrefs.GetInt("antienergy", 0) > 999)
            PlayerPrefs.SetInt("antienergy", 999);
        else PlayerPrefs.SetInt("antienergy", PlayerManager.countAntiEnergy + PlayerPrefs.GetInt("antienergy", 50));

        if (PlayerManager.countGoldenEnergy + PlayerPrefs.GetInt("goldenenergy", 0) > 999)
            PlayerPrefs.SetInt("goldenenergy", 999);
        else PlayerPrefs.SetInt("goldenenergy", PlayerManager.countGoldenEnergy + PlayerPrefs.GetInt("goldenenergy", 50));

        if (PlayerManager.countBlackHole + PlayerPrefs.GetInt("blackhole", 0) > 999)
            PlayerPrefs.SetInt("blackhole", 999);
        else PlayerPrefs.SetInt("blackhole", PlayerManager.countBlackHole + PlayerPrefs.GetInt("blackhole", 50));

        if (PlayerManager.countTimeFreeze + PlayerPrefs.GetInt("timefreeze", 0) > 999)
            PlayerPrefs.SetInt("timefreeze", 999);
        else PlayerPrefs.SetInt("timefreeze", PlayerManager.countTimeFreeze + PlayerPrefs.GetInt("timefreeze", 50));

        if (PlayerManager.countShield + PlayerPrefs.GetInt("shield", 0) > 999)
            PlayerPrefs.SetInt("shield", 999);
        else PlayerPrefs.SetInt("shield", PlayerManager.countShield + PlayerPrefs.GetInt("shield", 50));

    }

    void resetGame()
    {
        Time.timeScale = 1f;
        gameOver = false;
        nextTime = 0;
        gametime = 0;
        gameDifficulty = 1;
        EnemySpawner.spawnRate = 2.5f;
        endLevel = false;
        Camera.main.backgroundColor = menuColor;
    }

}
