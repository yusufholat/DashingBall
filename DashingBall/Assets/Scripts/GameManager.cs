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

    float gameTime = 0;
    public static int gameDifficulty = 1;

    public Gradient colors;
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
            setGameDifficulty(gameTime);
            gameTime += Time.deltaTime;
        }
        else
        {
            gameTime = 0;
        }

        if(gameStarded)
        Camera.main.backgroundColor = colors.Evaluate(Mathf.InverseLerp(0, 90, gameTime));

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
        Time.timeScale = 0.2f;
        gameOver = false;

        int newcoin = PlayerManager.score + PlayerPrefs.GetInt("TotalCoin", 3000);
        int highScore = PlayerPrefs.GetInt("HighScore", 0);

        if (PlayerManager.score > highScore)
            PlayerPrefs.SetInt("HighScore", PlayerManager.score);

        PlayerManager.totalCoin = newcoin;
        PlayerPrefs.SetInt("TotalCoin", newcoin);
    }

    public void restartGame()
    {
        Time.timeScale = 1f;
        gameOver = false;
        gameTime = 0;
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
        if (gameTime > 90)
        {
            EnemySpawner.spawnRate = 0.5f;
            gameDifficulty = 5;
        }
        else if(gameTime > 60)
        {
            EnemySpawner.spawnRate = 1f;
            gameDifficulty = 4;
        }
        else if (gameTime > 30)
        {
            EnemySpawner.spawnRate = 1.5f;
            gameDifficulty = 3;
        }
        else if (gameTime > 15)
        {
            EnemySpawner.spawnRate = 2f;
            gameDifficulty = 2;
        }
        else if(gameTime == 0){
            EnemySpawner.spawnRate = 2.5f;
            gameDifficulty = 1;
        }
    }

}
