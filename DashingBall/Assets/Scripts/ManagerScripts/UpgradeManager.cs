using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;



public class UpgradeManager : MonoBehaviour
{

    public GameObject shopItemTemplate;
    public Transform shopItemScrollView;

    public GameObject upgradeItemTemplate;
    public Transform upgradeItemScrollView;

    public GameObject profileStatTemplate;
    public Transform profileStatScroolView;


    GameObject item;
    GameObject upgradeITem;
    GameObject profileStat;

    Button upgradeButton;

    public static UpgradeManager instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {

    }

    IEnumerator listItems()
    {
        for (int i = 0; i < ItemManager.instance.ItemList.Count; i++)
        {
            yield return new WaitForSeconds(0.15f);
            item = Instantiate(shopItemTemplate, shopItemScrollView);
            item.transform.GetChild(0).GetComponent<Image>().sprite = ItemManager.instance.ItemList[i].image;
            item.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = ItemManager.instance.ItemList[i].count.ToString();
        }
    }

    IEnumerator listUpgradeItem()
    {
        for (int i = 0; i < ItemManager.instance.ItemList.Count; i++)
        {
            yield return new WaitForSeconds(0.15f);
            upgradeITem = Instantiate(upgradeItemTemplate, upgradeItemScrollView);
            upgradeITem.transform.GetChild(0).GetComponent<Image>().sprite = ItemManager.instance.ItemList[i].image;
            upgradeITem.transform.GetChild(2).GetChild(2).GetComponent<Image>().sprite = ItemManager.instance.ItemList[i].image;
            upgradeITem.transform.GetChild(2).GetComponent<Slider>().value = ItemManager.instance.ItemList[i].level;

            SetEffectUpgradeText(i);
            RefreshUpgradeCostAndButton(i);

            upgradeButton = upgradeITem.transform.GetChild(1).GetComponent<Button>();
            upgradeButton.AddEventListener(i, UpgradeButtonClicked);

        }

    }

    IEnumerator listStats()
    {
        for (int i = 0; i < ItemManager.instance.ItemList.Count; i++)
        {
            yield return new WaitForSeconds(0.15f);
            profileStat = Instantiate(profileStatTemplate, profileStatScroolView);
            profileStat.transform.GetChild(0).GetComponent<Image>().sprite = ItemManager.instance.ItemList[i].image;

            SetPlayerStatsCooldownText(i);
        }
    }




    private void UpgradeButtonClicked(int index)
    {
        if (HasEnoughGold(index, ItemManager.instance.ItemList[index].cost))
        {
            ItemManager.instance.ItemList[index].nextLevel(); //set next level for upgraded item

            ItemManager.instance.RefreshList(); //refresh counts,levels, costs, cooldowns

            upgradeItemScrollView.GetChild(index).GetChild(2).GetComponent<UpgradeBarController>().levelUp(); //levelbar refresh for upgraded item

            RefreshUpgradeCostAndButton(index); //refresh upgrade cost text, image, and button interactable
            RefreshItemCountText(index); // refresh currentitems after buy
            SetPlayerStatsCooldownText(index); //refresh playerstats cooldowntext

            if(ShopManager.instance != null)
            ShopManager.instance.RefreshItemCounts();

            if(ItemManager.instance.ItemList[index].level == Item.maxLevel)
                FindObjectOfType<AudioManager>().Play("UpgradeMax");
            else FindObjectOfType<AudioManager>().Play("Upgrade");


            upgradeItemScrollView.GetChild(index).GetComponent<Animator>().SetTrigger("upgrade");
        }
        else
        {
            upgradeItemScrollView.GetChild(index).GetComponent<Animator>().SetTrigger("shake");
            FindObjectOfType<AudioManager>().Play("Error");
        }
    }


    bool HasEnoughGold(int index, int prize)
    {
        if (ItemManager.instance.ItemList[index].name == "Energy")
        {
            if (PlayerPrefs.GetInt("energy", GameManager.defaultItemCounts) >= prize)
            {
                int totalEnergy = PlayerPrefs.GetInt("energy", GameManager.defaultItemCounts);
                totalEnergy -= prize;
                PlayerPrefs.SetInt("energy", totalEnergy);
                return true;
            }
            else return false;
        }

        else if (ItemManager.instance.ItemList[index].name == "AntiEnergy")
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

        else if (ItemManager.instance.ItemList[index].name == "GoldenEnergy")
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

        else if (ItemManager.instance.ItemList[index].name == "BlackHole")
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

        else if (ItemManager.instance.ItemList[index].name == "TimeFreeze")
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

        else if (ItemManager.instance.ItemList[index].name == "Shield")
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

   
    void RefreshUpgradeCostAndButton(int index)
    {
        if (ItemManager.instance.ItemList[index].level == Item.maxLevel)
        {
            upgradeItemScrollView.GetChild(index).GetChild(2).GetChild(4).GetComponent<TextMeshProUGUI>().text = "MAX!";
            upgradeItemScrollView.GetChild(index).GetChild(2).GetChild(3).gameObject.SetActive(false);
            upgradeItemScrollView.GetChild(index).GetChild(2).GetChild(2).gameObject.SetActive(false);
            upgradeItemScrollView.GetChild(index).GetChild(1).GetComponent<Button>().interactable = false;
            upgradeItemScrollView.GetChild(index).GetComponent<Animator>().SetTrigger("maxeffect");

        }
        else
        upgradeItemScrollView.GetChild(index).GetChild(2).GetChild(3).GetComponent<TextMeshProUGUI>().text = ItemManager.instance.ItemList[index].cost.ToString();
    }


    void RefreshItemCountText(int index)
    {
        shopItemScrollView.GetChild(index).GetChild(1).GetComponent<TextMeshProUGUI>().text = ItemManager.instance.ItemList[index].count.ToString();
    }



    void SetPlayerStatsCooldownText(int i)
    {
        if (ItemManager.instance.ItemList[i].name == "Energy")
            profileStatScroolView.GetChild(i).GetChild(1).GetComponent<TextMeshProUGUI>().text = "+" + ItemManager.instance.getCooldown("Energy").ToString() + " Enerji";

        else if (ItemManager.instance.ItemList[i].name == "AntiEnergy")
            profileStatScroolView.GetChild(i).GetChild(1).GetComponent<TextMeshProUGUI>().text = "-" + ItemManager.instance.getCooldown("AntiEnergy").ToString() + " Enerji";

        else if (ItemManager.instance.ItemList[i].name == "GoldenEnergy")
            profileStatScroolView.GetChild(i).GetChild(1).GetComponent<TextMeshProUGUI>().text = ItemManager.instance.getCooldown("GoldenEnergy").ToString() + " Saniye";

        else if (ItemManager.instance.ItemList[i].name == "BlackHole")
            profileStatScroolView.GetChild(i).GetChild(1).GetComponent<TextMeshProUGUI>().text = ItemManager.instance.getCooldown("BlackHole").ToString() + " Saniye";

        else if (ItemManager.instance.ItemList[i].name == "TimeFreeze")
            profileStatScroolView.GetChild(i).GetChild(1).GetComponent<TextMeshProUGUI>().text = ItemManager.instance.getCooldown("TimeFreeze").ToString() + " Saniye";

        else if (ItemManager.instance.ItemList[i].name == "Shield")
            profileStatScroolView.GetChild(i).GetChild(1).GetComponent<TextMeshProUGUI>().text = ItemManager.instance.getCooldown("Shield").ToString() + " Saniye";

    }


    void SetEffectUpgradeText(int index)
    {
        if (ItemManager.instance.ItemList[index].name == "Energy")
            upgradeItemScrollView.GetChild(index).GetChild(2).GetChild(4).GetComponent<TextMeshProUGUI>().text = "+10 Enerji->";

        else if (ItemManager.instance.ItemList[index].name == "AntiEnergy")
            upgradeItemScrollView.GetChild(index).GetChild(2).GetChild(4).GetComponent<TextMeshProUGUI>().text = "+10 Enerji->";

        else if (ItemManager.instance.ItemList[index].name == "GoldenEnergy")
            upgradeItemScrollView.GetChild(index).GetChild(2).GetChild(4).GetComponent<TextMeshProUGUI>().text = "+1 Saniye->";

        else if (ItemManager.instance.ItemList[index].name == "BlackHole")
            upgradeItemScrollView.GetChild(index).GetChild(2).GetChild(4).GetComponent<TextMeshProUGUI>().text = "+1 Saniye->";

        else if (ItemManager.instance.ItemList[index].name == "TimeFreeze")
            upgradeItemScrollView.GetChild(index).GetChild(2).GetChild(4).GetComponent<TextMeshProUGUI>().text = "+1 Saniye->";

        else if (ItemManager.instance.ItemList[index].name == "Shield")
            upgradeItemScrollView.GetChild(index).GetChild(2).GetChild(4).GetComponent<TextMeshProUGUI>().text = "+1 Saniye->";
    }

    public void RefreshItemCounts()
    {
        for (int i = 0; i < ItemManager.instance.ItemList.Count; i++)
        {
            if(shopItemScrollView.childCount != 0)
            shopItemScrollView.GetChild(i).GetChild(1).GetComponent<TextMeshProUGUI>().text = ItemManager.instance.ItemList[i].count.ToString();
        }
    }


    public void ListItemsAgain()
    {
        if (upgradeItemScrollView.childCount > 0)
        {
            for (int i = 0; i < ItemManager.instance.ItemList.Count; i++)
            {
                if (upgradeItemScrollView.GetChild(i) != null)
                    Destroy(upgradeItemScrollView.GetChild(i).gameObject);

                if (shopItemScrollView.GetChild(i) != null)
                    Destroy(shopItemScrollView.GetChild(i).gameObject);

                if (profileStatScroolView.GetChild(i) != null)
                    Destroy(profileStatScroolView.GetChild(i).gameObject);
            }
        }
        StartCoroutine(listItems());
        StartCoroutine(listUpgradeItem());
        StartCoroutine(listStats());
    }
}