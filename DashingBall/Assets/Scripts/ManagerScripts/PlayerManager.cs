using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;

    public GameObject playerTemplate;

    static GameObject instantiatedPlayer;
    
    public static float maxHealth;
    public static float currentHealth;
    public static int score;

    public static int totalCoin = 0;
    public static int countEnergy = 0;
    public static int countAntiEnergy = 0;
    public static int countGoldenEnergy = 0;
    public static int countBlackHole = 0;
    public static int countTimeFreeze = 0;
    public static int countShield = 0;


    public static bool goldenEnergyPower = false;
    public static bool timeFreezePower = false;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else            
            Destroy(gameObject);
    }

    void Start()
    {
        if (!GameManager.tutorialPlayerInstantiete)
            instantiatedPlayer = Instantiate(playerTemplate, new Vector2(0, -4), Quaternion.identity);
        totalCoin = PlayerPrefs.GetInt("TotalCoin", GameManager.defaultTotalCoin);
    }


    public void updateCurrentSkin(int skinIndex)
    {
        PlayerPrefs.SetInt("CurrentSkin", skinIndex);
    }

    public void RefreshSkin()
    {
        instantiatedPlayer.GetComponent<SpriteRenderer>().sprite = SkinManager.instance.ShopSkinList[PlayerPrefs.GetInt("CurrentSkin", 0)].image;
    }


    public void takeGoldenEnergy()
    {
        goldenEnergyPower = true;
        TakenSkillManager.instance.ShowSkill("GoldenEnergy");
        StartCoroutine(resetGoldenEnergy());
        countGoldenEnergy++;
    }

    IEnumerator resetGoldenEnergy()
    {
        yield return new WaitForSeconds(ItemManager.instance.getCooldown("GoldenEnergy"));
        EffectManager.PlayAnim("DefaultEffect");
        goldenEnergyPower = false;
    }

    public void takeBlackHole()
    {
        countBlackHole++;
        StartCoroutine(showblackhole());       
    }

    IEnumerator showblackhole()
    {
        yield return new WaitForSeconds(2f); // delay blackhole start anim
        TakenSkillManager.instance.ShowSkill("BlackHole");
    }




    public void takeTimeFreeze()
    {
        countTimeFreeze++;
        timeFreezePower = true;
        TakenSkillManager.instance.ShowSkill("TimeFreeze");
        StartCoroutine(resetTimeFreeze());
    }

    IEnumerator resetTimeFreeze()
    {
        yield return new WaitForSeconds(ItemManager.instance.getCooldown("TimeFreeze"));
        timeFreezePower = false;
    }




    public void takeShield()
    {
        countShield++;
        StartCoroutine(showbshield());
    }

    IEnumerator showbshield()
    {
        yield return new WaitForSeconds(1f); // delay shield start anim
        TakenSkillManager.instance.ShowSkill("Shield");
    }


    public void resetCounter()
    {
        countEnergy = 0;
        countAntiEnergy = 0;
        countGoldenEnergy = 0;
        countBlackHole = 0;
        countTimeFreeze = 0;
        countShield = 0;
    }

}
