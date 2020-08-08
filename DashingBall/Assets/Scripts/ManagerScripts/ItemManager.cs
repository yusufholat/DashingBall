using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ItemManager : MonoBehaviour
{
    public static ItemManager instance;

    private void Awake()
    {

        //PlayerPrefs.SetInt("energy", 500);
        //PlayerPrefs.SetInt("antienergy", 500);
        //PlayerPrefs.SetInt("goldenenergy", 500);
        //PlayerPrefs.SetInt("blackhole", 500);
        //PlayerPrefs.SetInt("timefreeze", 500);
        //PlayerPrefs.SetInt("shield", 500);

        RefreshList();

        if (instance == null)
            instance = this;
        else Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    public List <Item> ItemList;


    public Sprite getImage(string name)
    {
        Item[] ary = ItemList.ToArray();
        Item item = Array.Find(ary, s => s.name == name);
        if (item != null)
            return item.image;

        return ItemList[0].image;
    }

    public Sprite getSprite(string name)
    {
        Item[] ary = ItemList.ToArray();
        Item item = Array.Find(ary, s => s.name == name);
        if (item != null)
            return item.image;
        else return ItemList[0].image;
    }

    public float getCooldown(string name)
    {
        Item[] ary = ItemList.ToArray();
        Item item = Array.Find(ary, s => s.name == name);
        if (item != null)
            return item.cooldown;
        else return ItemList[0].cooldown;
    }


    public void RefreshList()
    {
        for (int i = 0; i < ItemList.Count; i++)
        {
            ItemList[i].RefreshCount();
            ItemList[i].RefreshLevel();
            ItemList[i].setLevelToCost();
            ItemList[i].setLevelToCooldown();
        }

    }

}