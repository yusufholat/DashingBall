using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public static class ButtonExtension
{
    public static void AddEventListener<T>(this Button button, T param, Action<T> onClick)
    {
        button.onClick.AddListener(delegate ()
        {
            onClick(param);
        });
    }
}

public class ShopManager : MonoBehaviour
{
    public static ShopManager instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else Destroy(gameObject);
    }

    List<ShopItem> ShopItemList;

    public GameObject shopItemTemplate;
    public Transform shopScrollView;

    GameObject item;
    Button buyButton;
    Button selectButton;

    public TextMeshProUGUI shopCoinText;

    void Start()
    {
        ShopItemList = SkinManager.instance.ShopItemList;
        int length = ShopItemList.Count;

        PlayerPrefs.SetInt("skin0", 1);
        for (int i = 0; i < length; i++)
        {
            item = Instantiate(shopItemTemplate, shopScrollView);
            item.transform.GetChild(0).GetComponent<Image>().sprite = ShopItemList[i].image;
            item.transform.GetChild(1).GetComponent<Button>().transform.GetChild(0).GetComponent<Text>().text = ShopItemList[i].prize.ToString();
            buyButton = item.transform.GetChild(1).GetComponent<Button>();
            selectButton = item.transform.GetChild(2).GetComponent<Button>();


            buyButton.gameObject.SetActive(PlayerPrefs.GetInt("skin" + i, 0) == 0);

            if (buyButton.gameObject.activeSelf == true)
            {
                selectButton.gameObject.SetActive(false);
            }
            else
            {
                selectButton.gameObject.SetActive(true);
            }
            
            buyButton.AddEventListener(i, ShopItemButtonClicked);
            selectButton.AddEventListener(i, ShopItemSelectClicked);
        }

        RefreshCoin();
    }

    void ShopItemButtonClicked(int itemIndex)
    {
        if (HasEnoughMoney(ShopItemList[itemIndex].prize))
        {
            useCash(ShopItemList[itemIndex].prize);
            buyButton = shopScrollView.GetChild(itemIndex).GetChild(1).GetComponent<Button>();
            selectButton = shopScrollView.GetChild(itemIndex).GetChild(2).GetComponent<Button>();
            buyButton.gameObject.SetActive(false);
            selectButton.gameObject.SetActive(true);
            PlayerPrefs.SetInt("skin" + itemIndex, 1);
            ShopItemList[itemIndex].isPurchased = true;
            RefreshCoin();
        }
    }
    void ShopItemSelectClicked(int itemIndex)
    {   
        PlayerPrefs.SetInt("CurrentSkinIndex", itemIndex);
        PlayerManager.instance.updateCurrentSkin(itemIndex);
        PlayerManager.instance.RefreshSkin();
    }

    void RefreshCoin()
    {
        shopCoinText.text = PlayerPrefs.GetInt("TotalCoin", 3000).ToString();
    }

    bool HasEnoughMoney(int prize)
    {
        return PlayerPrefs.GetInt("TotalCoin", 3000) >= prize;
    }

    void useCash(int prize)
    {
        int totalCoin = PlayerPrefs.GetInt("TotalCoin", 3000);
        //if (totalCoin >= prize)
            totalCoin -= prize;
        PlayerPrefs.SetInt("TotalCoin", totalCoin);
    }


}
