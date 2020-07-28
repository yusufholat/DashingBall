using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopSkinManager : MonoBehaviour
{

    public static ShopSkinManager instance;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    public List<ShopSkin> ShopSkinList;

}
