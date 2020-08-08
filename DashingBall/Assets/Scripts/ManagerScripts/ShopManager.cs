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

    //List<ShopSkin> ShopSkinList;
    //List<Item> ShopItemList;

    public GameObject shopSkinTemplate;
    public Transform shopSkinScrollView;

    public GameObject shopItemTemplate;
    public Transform shopItemScrollView;

    GameObject skin;
    GameObject item;

    Button buyButton;
    Button selectButton;

    public TextMeshProUGUI shopCoinText;


    public static ShopManager instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        PlayerPrefs.SetInt("skin0", 1); //set default skin true
        shopCoinText.text = PlayerPrefs.GetInt("TotalCoin", GameManager.defaultTotalCoin).ToString(); //refresh coin for cointext
    }


    IEnumerator listItem()
    {
        for (int i = 0; i < ItemManager.instance.ItemList.Count; i++)
        {
            yield return new WaitForSeconds(0.15f);
            item = Instantiate(shopItemTemplate, shopItemScrollView);
            item.transform.GetChild(0).GetComponent<Image>().sprite = ItemManager.instance.ItemList[i].image;
            item.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = ItemManager.instance.ItemList[i].count.ToString();
        }

    }

    IEnumerator listSkin()
    {
        for (int i = 0; i < SkinManager.instance.ShopSkinList.Count; i++)
        {
            yield return new WaitForSeconds(0.15f);
            skin = Instantiate(shopSkinTemplate, shopSkinScrollView);
            skin.transform.GetChild(0).GetComponent<Image>().sprite = SkinManager.instance.ShopSkinList[i].image;
            skin.transform.GetChild(1).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = SkinManager.instance.ShopSkinList[i].prize.ToString();
            setCostType(i);


            buyButton = skin.transform.GetChild(1).GetComponent<Button>();
            selectButton = skin.transform.GetChild(2).GetComponent<Button>();

            buyButton.gameObject.SetActive(PlayerPrefs.GetInt("skin" + i, 0) == 0);


            if (buyButton.gameObject.activeSelf == true)
                selectButton.gameObject.SetActive(false);

            else {
                selectButton.gameObject.SetActive(true);
                shopSkinScrollView.GetChild(i).GetComponent<Animator>().SetBool("buyanim", true);
            }

            if(i == PlayerPrefs.GetInt("CurrentSkin", 0))
            {
                selectButton = shopSkinScrollView.GetChild(i).GetChild(2).GetComponent<Button>(); //set current skin selected button to SELECTED text
                selectButton.gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "SELECTED";
                shopSkinScrollView.GetChild(i).GetComponent<Animator>().SetBool("buyanim", true);
            }

            buyButton.AddEventListener(i, ShopItemBuyButtonClicked);
            selectButton.AddEventListener(i, ShopItemSelectClicked);
        }

    }



    void ShopItemBuyButtonClicked(int itemIndex)
    {
        if (HasEnoughGold(itemIndex, SkinManager.instance.ShopSkinList[itemIndex].prize))
        {
            ItemManager.instance.RefreshList();

            shopSkinScrollView.GetChild(itemIndex).GetChild(1).GetComponent<Button>().gameObject.SetActive(false);
            shopSkinScrollView.GetChild(itemIndex).GetChild(2).GetComponent<Button>().gameObject.SetActive(true);
            PlayerPrefs.SetInt("skin" + itemIndex, 1); //set player prefs for bought skin
            RefreshGoldAndItemCounts(); // refresh gold and item counts for shop panel ui


            if (UpgradeManager.instance != null)
                UpgradeManager.instance.RefreshItemCounts();

            shopSkinScrollView.GetChild(itemIndex).GetComponent<Animator>().SetBool("buyanim", true);
            FindObjectOfType<AudioManager>().Play("Upgrade");
        }
        else
        {
            shopSkinScrollView.GetChild(itemIndex).GetComponent<Animator>().SetTrigger("nomoneyanim");
            FindObjectOfType<AudioManager>().Play("Error");
        }
    }

    void ShopItemSelectClicked(int itemIndex)
    {
        for (int i = 0; i < SkinManager.instance.ShopSkinList.Count; i++)
        {
            selectButton = shopSkinScrollView.GetChild(i).GetChild(2).GetComponent<Button>();
            selectButton.gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "SELECT";
            if (i == itemIndex)
            {
                selectButton = shopSkinScrollView.GetChild(i).GetChild(2).GetComponent<Button>();
                selectButton.gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "SELECTED";
                shopSkinScrollView.GetChild(i).GetComponent<Animator>().SetTrigger("buyanim");
            }
        }

        PlayerManager.instance.updateCurrentSkin(itemIndex); //selected current item index for player prefs
        PlayerManager.instance.RefreshSkin();
    }


    bool HasEnoughGold(int index, int prize)
    {
        if(SkinManager.instance.ShopSkinList[index].costType == "gold")
        {
            if (PlayerPrefs.GetInt("TotalCoin", GameManager.defaultTotalCoin) >= prize)
            {
                int totalCoin = PlayerPrefs.GetInt("TotalCoin", GameManager.defaultTotalCoin);
                totalCoin -= prize;
                PlayerPrefs.SetInt("TotalCoin", totalCoin);
                return true;
            }
            else return false;
        }

        else if (SkinManager.instance.ShopSkinList[index].costType == "energy")
        {
            if (PlayerPrefs.GetInt("energy", 50) >= prize)
            {
                int totalEnergy = PlayerPrefs.GetInt("energy", GameManager.defaultItemCounts);
                totalEnergy -= prize;
                PlayerPrefs.SetInt("energy", totalEnergy);
                return true;
            }
            else return false;
        }

        else if (SkinManager.instance.ShopSkinList[index].costType == "antienergy")
        {
            if (PlayerPrefs.GetInt("antienergy", GameManager.defaultItemCounts) >= prize)
            {
                int totalAntiEnergy = PlayerPrefs.GetInt("antienergy", GameManager.defaultItemCounts);
                totalAntiEnergy -= prize;
                PlayerPrefs.SetInt("antienergy", totalAntiEnergy);
                return true;
            }
            else return false;
        }

        else if (SkinManager.instance.ShopSkinList[index].costType == "goldenenergy")
        {
            if (PlayerPrefs.GetInt("goldenenergy", GameManager.defaultItemCounts) >= prize)
            {
                int totalGoldenEnergy = PlayerPrefs.GetInt("goldenenergy", GameManager.defaultItemCounts);
                totalGoldenEnergy -= prize;
                PlayerPrefs.SetInt("goldenenergy", totalGoldenEnergy);
                return true;
            }
            else return false;
        }

        else if (SkinManager.instance.ShopSkinList[index].costType == "blackhole")
        {
            if (PlayerPrefs.GetInt("blackhole", GameManager.defaultItemCounts) >= prize)
            {
                int totalBlackHole = PlayerPrefs.GetInt("blackhole", GameManager.defaultItemCounts);
                totalBlackHole -= prize;
                PlayerPrefs.SetInt("blackhole", totalBlackHole);
                return true;
            }
            else return false;
        }

        else if (SkinManager.instance.ShopSkinList[index].costType == "timefreeze")
        {
            if (PlayerPrefs.GetInt("timefreeze", GameManager.defaultItemCounts) >= prize)
            {
                int totalTimeFreeze = PlayerPrefs.GetInt("timefreeze", GameManager.defaultItemCounts);
                totalTimeFreeze -= prize;
                PlayerPrefs.SetInt("timefreeze", totalTimeFreeze);
                return true;
            }
            else return false;
        }

        else if (SkinManager.instance.ShopSkinList[index].costType == "shield")
        {
            if (PlayerPrefs.GetInt("shield", GameManager.defaultItemCounts) >= prize)
            {
                int totalShield = PlayerPrefs.GetInt("shield", GameManager.defaultItemCounts);
                totalShield -= prize;
                PlayerPrefs.SetInt("shield", totalShield);
                return true;
            }
            else return false;
        }

        else return false;
    }


    public void RefreshGoldAndItemCounts()
    {
        shopCoinText.text = PlayerPrefs.GetInt("TotalCoin", GameManager.defaultTotalCoin).ToString();

        shopItemScrollView.GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().text = PlayerPrefs.GetInt("energy", GameManager.defaultItemCounts).ToString();
        shopItemScrollView.GetChild(1).GetChild(1).GetComponent<TextMeshProUGUI>().text = PlayerPrefs.GetInt("antienergy", GameManager.defaultItemCounts).ToString();
        shopItemScrollView.GetChild(2).GetChild(1).GetComponent<TextMeshProUGUI>().text = PlayerPrefs.GetInt("goldenenergy", GameManager.defaultItemCounts).ToString();
        shopItemScrollView.GetChild(3).GetChild(1).GetComponent<TextMeshProUGUI>().text = PlayerPrefs.GetInt("blackhole", GameManager.defaultItemCounts).ToString();
        shopItemScrollView.GetChild(4).GetChild(1).GetComponent<TextMeshProUGUI>().text = PlayerPrefs.GetInt("timefreeze", GameManager.defaultItemCounts).ToString();
        shopItemScrollView.GetChild(5).GetChild(1).GetComponent<TextMeshProUGUI>().text = PlayerPrefs.GetInt("shield", GameManager.defaultItemCounts).ToString();
    }



    void setCostType(int index)
    {
        if (SkinManager.instance.ShopSkinList[index].costType == "energy")
            skin.transform.GetChild(1).transform.GetChild(1).GetComponent<Image>().sprite = ItemManager.instance.getImage("Energy");

        else if (SkinManager.instance.ShopSkinList[index].costType == "antienergy")
            skin.transform.GetChild(1).transform.GetChild(1).GetComponent<Image>().sprite = ItemManager.instance.getImage("AntiEnergy");

        else if (SkinManager.instance.ShopSkinList[index].costType == "goldenenergy")
            skin.transform.GetChild(1).transform.GetChild(1).GetComponent<Image>().sprite = ItemManager.instance.getImage("GoldenEnergy");

        else if (SkinManager.instance.ShopSkinList[index].costType == "blackhole")
            skin.transform.GetChild(1).transform.GetChild(1).GetComponent<Image>().sprite = ItemManager.instance.getImage("BlackHole");

        else if (SkinManager.instance.ShopSkinList[index].costType == "timefreeze")
            skin.transform.GetChild(1).transform.GetChild(1).GetComponent<Image>().sprite = ItemManager.instance.getImage("TimeFreeze");

        else if (SkinManager.instance.ShopSkinList[index].costType == "shield")
            skin.transform.GetChild(1).transform.GetChild(1).GetComponent<Image>().sprite = ItemManager.instance.getImage("Shield");

    }


    public void RefreshItemCounts()
    {
        for (int i = 0; i < ItemManager.instance.ItemList.Count; i++)
        {
            if (shopItemScrollView.childCount != 0)
                shopItemScrollView.GetChild(i).GetChild(1).GetComponent<TextMeshProUGUI>().text = ItemManager.instance.ItemList[i].count.ToString();
        }
    }

    public void ListItemsAgain()
    {
        if(shopSkinScrollView.childCount > 0)
        {
            for (int i = 0; i < SkinManager.instance.ShopSkinList.Count; i++)
            {
                if(shopSkinScrollView.GetChild(i) != null)
                    Destroy(shopSkinScrollView.GetChild(i).gameObject);
            }
            for (int i = 0; i < ItemManager.instance.ItemList.Count; i++)
            {
                if (shopItemScrollView.GetChild(i) != null)
                    Destroy(shopItemScrollView.GetChild(i).gameObject);
            }
        }
        StartCoroutine(listItem());
        StartCoroutine(listSkin());
    }

}