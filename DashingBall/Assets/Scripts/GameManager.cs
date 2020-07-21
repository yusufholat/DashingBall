using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{
    public static bool gameStarded;
    public static bool gamePaused = false;
    public static bool gameOver = false;

    public static bool musicOn;
    public static bool vibrationOn = true;
    public static bool MenuMusicPlaying;

    static GameManager instance;
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
    }

    private void Update()
    {
        if(musicOn == true && gameStarded == false && MenuMusicPlaying == false)
        {
            FindObjectOfType<AudioManager>().Play("MenuMusic");
            MenuMusicPlaying = true;
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

}
