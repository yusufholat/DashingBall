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

public class Shop : MonoBehaviour
{
    public static Shop instance;

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

    public TextMeshProUGUI coinText;

    void Start()
    {
        ShopItemList = SkinManager.instance.ShopItemList;
        int length = ShopItemList.Count;

        PlayerPrefs.SetInt("skin0", 1);
        for (int i = 0; i < length; i++)
        {
            item = Instantiate(shopItemTemplate, shopScrollView);
            item.transform.GetChild(0).GetComponent<Image>().sprite = ShopItemList[i].image;
            item.transform.GetChild(1).GetComponent<Text>().text = ShopItemList[i].prize.ToString();
            buyButton = item.transform.GetChild(2).GetComponent<Button>();
            selectButton = item.transform.GetChild(3).GetComponent<Button>();


            buyButton.interactable = PlayerPrefs.GetInt("skin" + i, 0) == 0;

            if (buyButton.interactable == true)
            {
                selectButton.interactable = false;
            }
            
            buyButton.AddEventListener(i, ShopItemButtonClicked);
            selectButton.AddEventListener(i, ShopItemSelectClicked);
        }

        RefreshCoin();
    }

    void ShopItemButtonClicked(int itemIndex)
    {
        if (PlayerManager.instance.HasEnoughMoney(ShopItemList[itemIndex].prize))
        {
            PlayerManager.instance.useCash(ShopItemList[itemIndex].prize);
            buyButton = shopScrollView.GetChild(itemIndex).GetChild(2).GetComponent<Button>();
            selectButton = shopScrollView.GetChild(itemIndex).GetChild(3).GetComponent<Button>();
            buyButton.interactable = false;
            selectButton.interactable = true;
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
        coinText.text = PlayerPrefs.GetInt("TotalCoin", 1000).ToString();
    }
}
