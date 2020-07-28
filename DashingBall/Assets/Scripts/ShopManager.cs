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

    List<ShopSkin> ShopSkinList;
    List<ShopItem> ShopItemList;

    public GameObject shopSkinTemplate;
    public Transform shopSkinScrollView;

    public GameObject shopItemTemplate;
    public Transform shopItemScrollView;

    GameObject skin;
    GameObject item;

    Button buyButton;
    Button selectButton;

    public TextMeshProUGUI shopCoinText;

    void Start()
    {

        ShopSkinList = ShopSkinManager.instance.ShopSkinList;
        ShopItemList = ShopItemManager.instance.ShopItemList;

        PlayerPrefs.SetInt("skin0", 1);
        shopCoinText.text = PlayerPrefs.GetInt("TotalCoin", 3000).ToString();

        for (int i = 0; i < ShopSkinList.Count; i++)
        {
            skin = Instantiate(shopSkinTemplate, shopSkinScrollView);
            skin.transform.GetChild(0).GetComponent<Image>().sprite = ShopSkinList[i].image;
            skin.transform.GetChild(1).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = ShopSkinList[i].prize.ToString();
            setCostType(i);




            buyButton = skin.transform.GetChild(1).GetComponent<Button>();
            selectButton = skin.transform.GetChild(2).GetComponent<Button>();


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

        for (int i = 0; i < ShopItemList.Count; i++)
        {
            item = Instantiate(shopItemTemplate, shopItemScrollView);
            item.transform.GetChild(0).GetComponent<Image>().sprite = ShopItemList[i].image;
            item.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = ShopItemList[i].count.ToString();
        }

        selectButton = shopSkinScrollView.GetChild(PlayerPrefs.GetInt("CurrentSkinIndex", 0)).GetChild(2).GetComponent<Button>();
        selectButton.gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "SELECTED";

        RefreshGold();
    }

    void ShopItemButtonClicked(int itemIndex)
    {
        Debug.Log("butontiklandi" + itemIndex);
        if (HasEnoughGold(itemIndex, ShopSkinList[itemIndex].prize))
        {
            shopSkinScrollView.GetChild(itemIndex).GetChild(1).GetComponent<Button>().gameObject.SetActive(false);
            shopSkinScrollView.GetChild(itemIndex).GetChild(2).GetComponent<Button>().gameObject.SetActive(true);
            PlayerPrefs.SetInt("skin" + itemIndex, 1);
            RefreshGold();
        }
    }

    void ShopItemSelectClicked(int itemIndex)
    {
        for (int i = 0; i < ShopSkinList.Count; i++)
        {

            selectButton = shopSkinScrollView.GetChild(i).GetChild(2).GetComponent<Button>();
            selectButton.gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "SELECT";
            if (i == itemIndex)
            {
                selectButton = shopSkinScrollView.GetChild(i).GetChild(2).GetComponent<Button>();
                selectButton.gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "SELECTED";
            }
        }

        PlayerPrefs.SetInt("CurrentSkinIndex", itemIndex);
        PlayerManager.instance.updateCurrentSkin(itemIndex);
        PlayerManager.instance.RefreshSkin();
    }


    void RefreshGold()
    {
        shopCoinText.text = PlayerPrefs.GetInt("TotalCoin", 3000).ToString();

        shopItemScrollView.GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().text = PlayerPrefs.GetInt("energy").ToString();
        shopItemScrollView.GetChild(1).GetChild(1).GetComponent<TextMeshProUGUI>().text = PlayerPrefs.GetInt("antienergy").ToString();
        shopItemScrollView.GetChild(2).GetChild(1).GetComponent<TextMeshProUGUI>().text = PlayerPrefs.GetInt("goldenenergy").ToString();
        shopItemScrollView.GetChild(3).GetChild(1).GetComponent<TextMeshProUGUI>().text = PlayerPrefs.GetInt("blackhole").ToString();
        shopItemScrollView.GetChild(4).GetChild(1).GetComponent<TextMeshProUGUI>().text = PlayerPrefs.GetInt("timefreeze").ToString();
        shopItemScrollView.GetChild(5).GetChild(1).GetComponent<TextMeshProUGUI>().text = PlayerPrefs.GetInt("shield").ToString();
    }

    bool HasEnoughGold(int index, int prize)
    {
        if(ShopSkinList[index].costType == "gold")
        {
            Debug.Log("turu gold");
            if (PlayerPrefs.GetInt("TotalCoin", 3000) >= prize)
            {
                int totalCoin = PlayerPrefs.GetInt("TotalCoin", 3000);
                totalCoin -= prize;
                PlayerPrefs.SetInt("TotalCoin", totalCoin);
                Debug.Log("satinalindi");
                return true;
            }
            else return false;
        }

        else if (ShopSkinList[index].costType == "energy")
        {
            if (PlayerPrefs.GetInt("energy", 0) >= prize)
            {
                int totalEnergy = PlayerPrefs.GetInt("energy", 0);
                totalEnergy -= prize;
                PlayerPrefs.SetInt("energy", totalEnergy);
                return true;
            }
            else return false;
        }

        else if (ShopSkinList[index].costType == "antienergy")
        {
            if (PlayerPrefs.GetInt("antienergy", 0) >= prize)
            {
                int totalAntiEnergy = PlayerPrefs.GetInt("antienergy", 0);
                totalAntiEnergy -= prize;
                PlayerPrefs.SetInt("antienergy", totalAntiEnergy);
                return true;
            }
            else return false;
        }

        else if (ShopSkinList[index].costType == "goldenenergy")
        {
            if (PlayerPrefs.GetInt("goldenenergy", 0) >= prize)
            {
                int totalGoldenEnergy = PlayerPrefs.GetInt("goldenenergy", 0);
                totalGoldenEnergy -= prize;
                PlayerPrefs.SetInt("goldenenergy", totalGoldenEnergy);
                return true;
            }
            else return false;
        }

        else if (ShopSkinList[index].costType == "blackhole")
        {
            if (PlayerPrefs.GetInt("blackhole", 0) >= prize)
            {
                int totalBlackHole = PlayerPrefs.GetInt("blackhole", 0);
                totalBlackHole -= prize;
                PlayerPrefs.SetInt("blackhole", totalBlackHole);
                return true;
            }
            else return false;
        }

        else if (ShopSkinList[index].costType == "timefreeze")
        {
            if (PlayerPrefs.GetInt("timefreeze", 0) >= prize)
            {
                int totalTimeFreeze = PlayerPrefs.GetInt("timefreeze", 0);
                totalTimeFreeze -= prize;
                PlayerPrefs.SetInt("timefreeze", totalTimeFreeze);
                return true;
            }
            else return false;
        }

        else if (ShopSkinList[index].costType == "shield")
        {
            if (PlayerPrefs.GetInt("shield", 0) >= prize)
            {
                int totalShield = PlayerPrefs.GetInt("shield", 0);
                totalShield -= prize;
                PlayerPrefs.SetInt("shield", totalShield);
                return true;
            }
            else return false;
        }

        else return false;
    }

    void setCostType(int index)
    {
        if (ShopSkinList[index].costType == "energy")
            skin.transform.GetChild(1).transform.GetChild(1).GetComponent<Image>().sprite = ShopItemManager.instance.getImage("itemEnergy");
        else if (ShopSkinList[index].costType == "antienergy")
            skin.transform.GetChild(1).transform.GetChild(1).GetComponent<Image>().sprite = ShopItemManager.instance.getImage("itemAntiEnergy");
        else if (ShopSkinList[index].costType == "goldenenergy")
            skin.transform.GetChild(1).transform.GetChild(1).GetComponent<Image>().sprite = ShopItemManager.instance.getImage("itemGoldenEnergy");
        else if (ShopSkinList[index].costType == "blackhole")
            skin.transform.GetChild(1).transform.GetChild(1).GetComponent<Image>().sprite = ShopItemManager.instance.getImage("itemBlackHole");
        else if (ShopSkinList[index].costType == "timefreeze")
            skin.transform.GetChild(1).transform.GetChild(1).GetComponent<Image>().sprite = ShopItemManager.instance.getImage("itemTimeFreeze");
        else if (ShopSkinList[index].costType == "shield")
            skin.transform.GetChild(1).transform.GetChild(1).GetComponent<Image>().sprite = ShopItemManager.instance.getImage("itemShield");
    }

}