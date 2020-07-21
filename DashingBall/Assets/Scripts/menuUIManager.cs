using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class menuUIManager : MonoBehaviour
{
    private Animator transition;

    public GameObject StartMenu;
    public GameObject SettingsMenu;
    public GameObject ShopMenuPanel;

    Animator settingMenuAnimator;

    public GameObject musicButton;
    public GameObject vibrationButton;

    public Sprite MusicOnSprite;
    public Sprite MusicOffSprite;
    public Sprite VibrationOnSprite;
    public Sprite VibrationOffSprite;

    bool settingsMenuIsOpen = false;
    public static bool shopIsOpen = false;

    private void Awake()
    {
        settingMenuAnimator = SettingsMenu.GetComponent<Animator>();
        transition = GetComponent<Animator>();
    }
    public void playGame()
    {      
        StartCoroutine(LoadScene());      
    }
      
    IEnumerator LoadScene()
    {
        transition.SetTrigger("end");
        yield return new WaitForSeconds(1f);
        FindObjectOfType<AudioManager>().Stop("MenuMusic");
        GameManager.MenuMusicPlaying = false;
        GameManager.gameStarded = true;
        SceneManager.LoadScene("Game");
    }

    private void Update()
    {
        if(GameManager.musicOn == true)
            musicButton.GetComponent<UnityEngine.UI.Image>().sprite = MusicOnSprite;
        else
            musicButton.GetComponent<UnityEngine.UI.Image>().sprite = MusicOffSprite;

        if (GameManager.vibrationOn == true)
            vibrationButton.GetComponent<UnityEngine.UI.Image>().sprite = VibrationOnSprite;
        else
            vibrationButton.GetComponent<UnityEngine.UI.Image>().sprite = VibrationOffSprite;
    }

    public void settingsOnOff()
    {
        if (settingsMenuIsOpen == false)
        {
            settingMenuAnimator.SetTrigger("on");
            settingsMenuIsOpen = true;
        }
        else
        {
            settingMenuAnimator.SetTrigger("off");
            settingsMenuIsOpen = false;
        }

    }

    public void musicOnOff()
    {
        if (GameManager.musicOn == true)
        {
            FindObjectOfType<AudioManager>().Stop("MenuMusic");
            GameManager.musicOn = false;
            PlayerPrefs.SetInt("MusicOn", GameManager.musicOn ? 1 : 0);
        }
        else
        {
            FindObjectOfType<AudioManager>().Play("MenuMusic");
            GameManager.musicOn = true;
            PlayerPrefs.SetInt("MusicOn", GameManager.musicOn ? 1 : 0);
        }
    }

    public void vibrationOnOff()
    {
        if (GameManager.vibrationOn == true)
            GameManager.vibrationOn = false;
        else GameManager.vibrationOn = true;        
    }

    public void openShop()
    {    
        StartMenu.SetActive(false);
        ShopMenuPanel.SetActive(true);

        if(settingsMenuIsOpen == true)
        {
            settingMenuAnimator.SetTrigger("off");
            settingsMenuIsOpen = false;
        }
        shopIsOpen = true;
    }

    public void closeShop()
    {
        ShopMenuPanel.SetActive(false);
        StartMenu.SetActive(true);
        shopIsOpen = false;
    }
}
