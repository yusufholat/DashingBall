using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeBarController : MonoBehaviour
{
    UpgradeBar upgradeBar;

    void Awake()
    {
        upgradeBar = GetComponent<UpgradeBar>();
    }

    private void Start()
    {
        upgradeBar.SetMaxLevel(Item.maxLevel);    
    }

    public void levelUp()
    {
        upgradeBar.SliderNextValue();
    }
}
