using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ShopItemManager : MonoBehaviour
{
    public static ShopItemManager instance;
    private void Awake()
    {
        ShopItemList[0].count = PlayerPrefs.GetInt("energy", 0);
        ShopItemList[1].count = PlayerPrefs.GetInt("antienergy", 0);
        ShopItemList[2].count = PlayerPrefs.GetInt("goldenenergy", 0);
        ShopItemList[3].count = PlayerPrefs.GetInt("blackhole", 0);
        ShopItemList[4].count = PlayerPrefs.GetInt("timefreeze", 0);
        ShopItemList[5].count = PlayerPrefs.GetInt("shield", 0);

        if (instance == null)
            instance = this;
        else Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    public List<ShopItem> ShopItemList;

    private void Start()
    {
        //for (int i = 0; i < ShopItemList.Count; i++)
        //{
        //    ShopItemList[i].count = PlayerPrefs.GetInt("countItem" + i, 0);
        //}
    }

    public Sprite getImage(string name)
    {
        ShopItem[] ary = ShopItemList.ToArray();
        ShopItem item = Array.Find(ary, s => s.name == name);
        if(item !=null)
        return item.image;

        return ShopItemList[0].image;
    }
}