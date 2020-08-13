using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

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

    public static UIManager instance;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else Destroy(gameObject);
    }
    private void Start()
    {
        transition = GetComponent<Animator>();    
    }

    private void Update()
    {

    }

    public void ShowGameOver()
    {
        scoreTextUI.SetActive(false);

        gameOverScoreText.text = PlayerManager.score.ToString();
        gameOverHighScoreText.text = PlayerPrefs.GetInt("HighScore", 0).ToString();

        countEnergyText.text = "x" + PlayerManager.countEnergy.ToString();
        countAntiEnergyText.text = "x" + PlayerManager.countAntiEnergy.ToString();
        countGoldenEnergyText.text = "x" + PlayerManager.countGoldenEnergy.ToString();
        countBlackHoleText.text = "x" + PlayerManager.countBlackHole.ToString();
        countTimeFreezeText.text = "x" + PlayerManager.countTimeFreeze.ToString();
        countShieldText.text = "x" + PlayerManager.countShield.ToString();

        gameOverMenu.SetActive(true);
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

    public void SSAndShare()
    {
        StartCoroutine(TakeScreenshotAndShare());
    }
    IEnumerator TakeScreenshotAndShare()
    {
        yield return new WaitForEndOfFrame();

        Texture2D ss = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        ss.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        ss.Apply();

        string filePath = Path.Combine(Application.temporaryCachePath, "shared.png");
        File.WriteAllBytes(filePath, ss.EncodeToPNG());

        Destroy(ss);

        new NativeShare().AddFile(filePath)
            .SetSubject("DashingBall").SetText("Bu rekorun vergisini vermek lazım")
            .SetCallback((result, shareTarget) => Debug.Log("Share result: " + result + ", selected app: " + shareTarget))
            .Share();

        // Share on WhatsApp only, if installed (Android only)
        //if( NativeShare.TargetExists( "com.whatsapp" ) )
        //	new NativeShare().AddFile( filePath ).AddTarget( "com.whatsapp" ).Share();
    }
}
