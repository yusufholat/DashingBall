using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class menuUIManager : MonoBehaviour
{
    private Animator transition;

    public GameObject StartMenu;
    public GameObject SettingsMenu;

    public AudioSource audio;

    bool settingsIsOpen = false;

    private void Awake()
    {
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
        SceneManager.LoadScene("Game");
    }

    public void gotoSettings()
    {
        if (settingsIsOpen == false)
        {
            SettingsMenu.SetActive(true);
            settingsIsOpen = true;
        } 
        else {
            SettingsMenu.SetActive(false);
            settingsIsOpen = false;
        } 
        
    }

    public void musicOnOff()
    {
        if (audio.isPlaying == true)
            audio.Pause();
        else audio.Play();
    }
}
