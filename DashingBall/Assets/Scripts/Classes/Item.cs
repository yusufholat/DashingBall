using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;


[System.Serializable]

public class Item
{
    public static int maxLevel = 5;

    public string name;
    public Sprite image;

    public int count;
    public int level = 1;
    public float cooldown;
    public int cost;


    public void nextLevel()
    {
        if (level < maxLevel)
            level++;

        RefreshLevel();
        setLevelToCost();
        setLevelToCooldown();
        setNextLevelForPlayerPrefs();
    }



    public void RefreshCount()
    {
        if (name == "Energy")
            count = PlayerPrefs.GetInt("energy", GameManager.defaultItemCounts);

        else if (name == "AntiEnergy")
            count = PlayerPrefs.GetInt("antienergy", GameManager.defaultItemCounts);

        else if (name == "GoldenEnergy")
            count = PlayerPrefs.GetInt("goldenenergy", GameManager.defaultItemCounts);

        else if (name == "BlackHole")
            count = PlayerPrefs.GetInt("blackhole", GameManager.defaultItemCounts);

        else if (name == "TimeFreeze")
            count = PlayerPrefs.GetInt("timefreeze", GameManager.defaultItemCounts);

        else if (name == "Shield")
            count = PlayerPrefs.GetInt("shield", GameManager.defaultItemCounts);
    }


    public void RefreshLevel()
    {
        if (name == "Energy")
            level = PlayerPrefs.GetInt("currentenergylevel", 1);

        else if (name == "AntiEnergy")
            level = PlayerPrefs.GetInt("currentantienergylevel", 1);

        else if (name == "GoldenEnergy")
            level = PlayerPrefs.GetInt("currentgoldenenergylevel", 1);

        else if (name == "BlackHole")
            level = PlayerPrefs.GetInt("currentblackholelevel", 1);

        else if (name == "TimeFreeze")
            level = PlayerPrefs.GetInt("currenttimefreezelevel", 1);

        else if (name == "Shield")
            level = PlayerPrefs.GetInt("currentshieldlevel", 1);
    }



    public void setLevelToCost()
    {
        if (level == 1) cost = 10;

        else if (level == 2) cost = 25;

        else if (level == 3) cost = 50;

        else if (level == 4) cost = 100;

        else if (level == 5) cost = 0;
    }



    public void setLevelToCooldown()
    {
        if (level == 1) cooldown = 4;
        else if (level == 2) cooldown = 5;
        else if (level == 3) cooldown = 6;
        else if (level == 4) cooldown = 7;
        else if (level == 5) cooldown = 8;

        if (name == "Energy")
        {
            if (level == 1) cooldown = 50;
            else if (level == 2) cooldown = 60;
            else if (level == 3) cooldown = 70;
            else if (level == 4) cooldown = 80;
            else if (level == 5) cooldown = 90;
        }

        if (name == "AntiEnergy")
        {
            if (level == 1) cooldown = 50;
            else if (level == 2) cooldown = 40;
            else if (level == 3) cooldown = 30;
            else if (level == 4) cooldown = 20;
            else if (level == 5) cooldown = 10;
        }

    }


    void setNextLevelForPlayerPrefs()
    {
        if (level < maxLevel)
        {
            if (name == "Energy")
            {
                int oldlevel = PlayerPrefs.GetInt("currentenergylevel", 1);
                PlayerPrefs.SetInt("currentenergylevel", oldlevel + 1);
            }
            else if (name == "AntiEnergy")
            {
                int oldlevel = PlayerPrefs.GetInt("currentantienergylevel", 1);
                PlayerPrefs.SetInt("currentantienergylevel", oldlevel + 1);
            }
            else if (name == "GoldenEnergy")
            {
                int oldlevel = PlayerPrefs.GetInt("currentgoldenenergylevel", 1);
                PlayerPrefs.SetInt("currentgoldenenergylevel", oldlevel + 1);
            }
            else if (name == "BlackHole")
            {
                int oldlevel = PlayerPrefs.GetInt("currentblackholelevel", 1);
                PlayerPrefs.SetInt("currentblackholelevel", oldlevel + 1);
            }
            else if (name == "TimeFreeze")
            {
                int oldlevel = PlayerPrefs.GetInt("currenttimefreezelevel", 1);
                PlayerPrefs.SetInt("currenttimefreezelevel", oldlevel + 1);
            }
            else if (name == "Shield")
            {
                int oldlevel = PlayerPrefs.GetInt("currentshieldlevel", 1);
                PlayerPrefs.SetInt("currentshieldlevel", oldlevel + 1);
            }
        }

    }
}
