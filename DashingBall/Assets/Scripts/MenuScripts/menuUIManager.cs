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
    public GameObject ShopMenu;

    Animator settingMenuAnimator;
    Animator shopMenuAnimator;

    public GameObject musicButton;
    public GameObject vibrationButton;

    public Sprite MusicOnSprite;
    public Sprite MusicOffSprite;
    public Sprite VibrationOnSprite;
    public Sprite VibrationOffSprite;

    public GameObject shopSkinTopPanel;
    public GameObject shopSkinBottomPanel;
    public GameObject shopUpgradeTopPanel;
    public GameObject shopUpgradeBottomPanel;

    bool settingsMenuIsOpen = false;
    bool shopMenuIsOpen = false;

    public static bool shopIsOpen = false;

    private void Awake()
    {
        settingMenuAnimator = SettingsMenu.GetComponent<Animator>();
        shopMenuAnimator = ShopMenu.GetComponent<Animator>();
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

    public void OpenTutorial()
    {
        StartCoroutine(LoadTutorial());
    }

    IEnumerator LoadTutorial()
    {
        transition.SetTrigger("tutorial");
        yield return new WaitForSeconds(1f);
        FindObjectOfType<AudioManager>().Stop("MenuMusic");
        GameManager.MenuMusicPlaying = true;
        GameManager.tutorialStarted = true;
        GameManager.tutorialInstantieted = true;
        SceneManager.LoadScene("Tutorial");
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

    public void shopOnOff()
    {
        if (shopMenuIsOpen == false)
        {
            shopMenuAnimator.SetTrigger("on");
            shopMenuIsOpen = true;
        }
        else
        {
            shopMenuAnimator.SetTrigger("off");
            shopMenuIsOpen = false;
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

    public void openSkinShop()
    {
        transition.SetTrigger("openshop");
        if(settingsMenuIsOpen == true)
        {
            settingMenuAnimator.SetTrigger("off");
            settingsMenuIsOpen = false;
        }
        if (shopMenuIsOpen == true)
        {
            shopMenuAnimator.SetTrigger("off");
            shopMenuIsOpen = false;
        }
        shopIsOpen = true;
    }

    public void closeSkinShop()
    {
        shopSkinTopPanel.gameObject.tag = "ShopOutsideArea";
        shopSkinTopPanel.GetComponent<BoxCollider2D>().isTrigger = true;
        shopSkinBottomPanel.gameObject.tag = "ShopOutsideArea";
        shopSkinBottomPanel.GetComponent<BoxCollider2D>().isTrigger = true;

        transition.SetTrigger("closeshop");
        shopIsOpen = false;
    }


    public void openUpgradeShop()
    {
        transition.SetTrigger("openupgrade");
        if (settingsMenuIsOpen == true)
        {
            settingMenuAnimator.SetTrigger("off");
            settingsMenuIsOpen = false;
        }
        if (shopMenuIsOpen == true)
        {
            shopMenuAnimator.SetTrigger("off");
            shopMenuIsOpen = false;
        }
        shopIsOpen = true;
    }

    public void closeUpgradeShop()
    {
        shopUpgradeTopPanel.gameObject.tag = "ShopOutsideArea";
        shopUpgradeTopPanel.GetComponent<BoxCollider2D>().isTrigger = true;
        shopUpgradeBottomPanel.gameObject.tag = "ShopOutsideArea";
        shopUpgradeBottomPanel.GetComponent<BoxCollider2D>().isTrigger = true;

        transition.SetTrigger("closeupgrade");
        shopIsOpen = false;
    }
}
