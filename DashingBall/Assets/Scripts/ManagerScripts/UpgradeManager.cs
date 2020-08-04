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

    void Start()
    {
        //item list
        for (int i = 0; i < ItemManager.instance.ItemList.Count; i++)
        {
            item = Instantiate(shopItemTemplate, shopItemScrollView);
            item.transform.GetChild(0).GetComponent<Image>().sprite = ItemManager.instance.ItemList[i].image;
            item.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = ItemManager.instance.ItemList[i].count.ToString();
        }

        //upgrade list 
        for (int i = 0; i < ItemManager.instance.ItemList.Count; i++)
        {
            upgradeITem = Instantiate(upgradeItemTemplate, upgradeItemScrollView);
            upgradeITem.transform.GetChild(0).GetComponent<Image>().sprite = ItemManager.instance.ItemList[i].image;
            upgradeITem.transform.GetChild(2).GetChild(2).GetComponent<Image>().sprite = ItemManager.instance.ItemList[i].image;
            upgradeITem.transform.GetChild(2).GetComponent<Slider>().value = ItemManager.instance.ItemList[i].level;

            RefreshUpgradeCostAndButton(i);

            upgradeButton = upgradeITem.transform.GetChild(1).GetComponent<Button>();
            upgradeButton.AddEventListener(i, UpgradeButtonClicked);
        }

        //playerstats list
        for (int i = 0; i < ItemManager.instance.ItemList.Count; i++)
        {
            profileStat = Instantiate(profileStatTemplate, profileStatScroolView);
            profileStat.transform.GetChild(0).GetComponent<Image>().sprite = ItemManager.instance.ItemList[i].image;

            SetPlayerStatsCooldownText(i);
        }

    }

    private void UpgradeButtonClicked(int index)
    {
        if (HasEnoughGold(index, ItemManager.instance.ItemList[index].cost))
        {
            ItemManager.instance.ItemList[index].nextLevel();

            ItemManager.instance.RefreshList();

            upgradeItemScrollView.GetChild(index).GetChild(2).GetComponent<UpgradeBarController>().levelUp();

            RefreshUpgradeCostAndButton(index); //refresh upgrade cost text, image, and button interactable
            RefreshItemCountText(index); // refresh currentitems after buy
            SetPlayerStatsCooldownText(index); //refresh playerstats cooldowntext
        }
    }


    bool HasEnoughGold(int index, int prize)
    {
        if (ItemManager.instance.ItemList[index].name == "Energy")
        {
            if (PlayerPrefs.GetInt("energy", 50) >= prize)
            {
                int totalEnergy = PlayerPrefs.GetInt("energy", 50);
                totalEnergy -= prize;
                PlayerPrefs.SetInt("energy", totalEnergy);
                return true;
            }
            else return false;
        }

        else if (ItemManager.instance.ItemList[index].name == "AntiEnergy")
        {
            if (PlayerPrefs.GetInt("antienergy", 50) >= prize)
            {
                int totalAntiEnergy = PlayerPrefs.GetInt("antienergy", 50);
                totalAntiEnergy -= prize;
                PlayerPrefs.SetInt("antienergy", totalAntiEnergy);
                return true;
            }
            else return false;
        }

        else if (ItemManager.instance.ItemList[index].name == "GoldenEnergy")
        {
            if (PlayerPrefs.GetInt("goldenenergy", 50) >= prize)
            {
                int totalGoldenEnergy = PlayerPrefs.GetInt("goldenenergy", 50);
                totalGoldenEnergy -= prize;
                PlayerPrefs.SetInt("goldenenergy", totalGoldenEnergy);
                return true;
            }
            else return false;
        }

        else if (ItemManager.instance.ItemList[index].name == "BlackHole")
        {
            if (PlayerPrefs.GetInt("blackhole", 50) >= prize)
            {
                int totalBlackHole = PlayerPrefs.GetInt("blackhole", 50);
                totalBlackHole -= prize;
                PlayerPrefs.SetInt("blackhole", totalBlackHole);
                return true;
            }
            else return false;
        }

        else if (ItemManager.instance.ItemList[index].name == "TimeFreeze")
        {
            if (PlayerPrefs.GetInt("timefreeze", 50) >= prize)
            {
                int totalTimeFreeze = PlayerPrefs.GetInt("timefreeze", 50);
                totalTimeFreeze -= prize;
                PlayerPrefs.SetInt("timefreeze", totalTimeFreeze);
                return true;
            }
            else return false;
        }

        else if (ItemManager.instance.ItemList[index].name == "Shield")
        {
            if (PlayerPrefs.GetInt("shield", 50) >= prize)
            {
                int totalShield = PlayerPrefs.GetInt("shield", 50);
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
            upgradeItemScrollView.GetChild(index).GetChild(2).GetChild(3).GetComponent<TextMeshProUGUI>().text = "MAX!";
            upgradeItemScrollView.GetChild(index).GetChild(2).GetChild(2).gameObject.SetActive(false);
            upgradeItemScrollView.GetChild(index).GetChild(1).GetComponent<Button>().interactable = false;
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
            profileStatScroolView.GetChild(i).GetChild(1).GetComponent<TextMeshProUGUI>().text = "+" + ItemManager.instance.getCooldown("Energy").ToString() + " Energy";

        else if (ItemManager.instance.ItemList[i].name == "AntiEnergy")
            profileStatScroolView.GetChild(i).GetChild(1).GetComponent<TextMeshProUGUI>().text = "-" + ItemManager.instance.getCooldown("AntiEnergy").ToString() + " Energy";

        else if (ItemManager.instance.ItemList[i].name == "GoldenEnergy")
            profileStatScroolView.GetChild(i).GetChild(1).GetComponent<TextMeshProUGUI>().text = ItemManager.instance.getCooldown("GoldenEnergy").ToString() + " Second";

        else if (ItemManager.instance.ItemList[i].name == "BlackHole")
            profileStatScroolView.GetChild(i).GetChild(1).GetComponent<TextMeshProUGUI>().text = ItemManager.instance.getCooldown("BlackHole").ToString() + " Second";

        else if (ItemManager.instance.ItemList[i].name == "TimeFreeze")
            profileStatScroolView.GetChild(i).GetChild(1).GetComponent<TextMeshProUGUI>().text = ItemManager.instance.getCooldown("TimeFreeze").ToString() + " Second";

        else if (ItemManager.instance.ItemList[i].name == "Shield")
            profileStatScroolView.GetChild(i).GetChild(1).GetComponent<TextMeshProUGUI>().text = ItemManager.instance.getCooldown("Shield").ToString() + " Second";

    }

}