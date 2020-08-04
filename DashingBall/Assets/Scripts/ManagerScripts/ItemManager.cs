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
        RefreshCounts();
        RefreshLevels();
        RefreshCosts();
        RefreshCoolDowns();
    }

    void RefreshCounts()
    {
        for (int i = 0; i < ItemList.Count; i++)
        {
            ItemList[i].RefreshCount();
        }
    }

    void RefreshLevels()
    {
        for (int i = 0; i < ItemList.Count; i++)
        {
            ItemList[i].RefreshLevel();
        }
    }

    void RefreshCosts()
    {
        for (int i = 0; i < ItemList.Count; i++)
        {
            ItemList[i].setLevelToCost();
        }
    }

    void RefreshCoolDowns()
    {
        for (int i = 0; i < ItemList.Count; i++)
        {
            ItemList[i].setLevelToCooldown();
        }
    }

}