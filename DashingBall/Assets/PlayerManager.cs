using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;

    public static int totalCoin;

    public GameObject playerTemplate;

    public static GameObject instantiatedPlayer;

    public static float maxHealth;
    public static float currentHealth;
    public static int score;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else            
            Destroy(gameObject);

        instantiatedPlayer = Instantiate(playerTemplate, transform.position, Quaternion.identity);

        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        totalCoin = PlayerPrefs.GetInt("TotalCoin", 1000);
    }

    public void useCash(int prize)
    {
        if (totalCoin >= prize)
            totalCoin -= prize;
            PlayerPrefs.SetInt("TotalCoin", totalCoin);
    }

    public bool HasEnoughMoney(int prize)
    {
        return PlayerPrefs.GetInt("TotalCoin", 1000) >= prize;
    }

    public void updateCurrentSkin(int skinIndex)
    {
        PlayerPrefs.SetInt("CurrentSkin", skinIndex);
    }

    public void RefreshSkin()
    {
        instantiatedPlayer.GetComponent<SpriteRenderer>().sprite = SkinManager.instance.ShopItemList[PlayerPrefs.GetInt("CurrentSkin", 0)].image;
    }
}
