using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;

public class PlayerGPSManager : MonoBehaviour
{
    public static PlayerGPSManager instance;
    public static PlayGamesPlatform platform;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }
    void Start()
    {
        StartCoroutine(StartWithDelay());
    }
    IEnumerator StartWithDelay()
    {
        yield return new WaitForSeconds(1.5f);
        if (platform == null)
        {
            PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().Build();

            PlayGamesPlatform.InitializeInstance(config);
            PlayGamesPlatform.DebugLogEnabled = true;

            platform = PlayGamesPlatform.Activate();
        }

        Social.Active.localUser.Authenticate(success =>
        {
            if (success)
            {
                Debug.Log("logged in basarili bro");
            }
            else Debug.Log("not logged in bro");
        });
    }


    public void ShowLeaderBoard()
    {
        Social.Active.localUser.Authenticate(success =>
        {
            if (success)
            {
                Social.ShowLeaderboardUI();
            }
            else platform = PlayGamesPlatform.Activate();
        });
    }

    public void UpdateLeaderBoardScore(int highscore)
    {
        Social.ReportScore(highscore, GPGSIds.leaderboard_high_score, (bool success) =>
        {
            Debug.Log("score guncellendi bro");
        });
    }
}
