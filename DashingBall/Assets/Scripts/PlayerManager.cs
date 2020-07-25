using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;

    public GameObject playerTemplate;

    public static GameObject instantiatedPlayer;

    public static int totalCoin;
    public static float maxHealth;
    public static float currentHealth;
    public static int score;


    public static bool goldenEnergyPower = false;
    public static bool timeFreezePower = false;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else            
            Destroy(gameObject);

        instantiatedPlayer = Instantiate(playerTemplate, new Vector2(0, -2), Quaternion.identity);

        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        totalCoin = PlayerPrefs.GetInt("TotalCoin", 1000);
    }


    public void updateCurrentSkin(int skinIndex)
    {
        PlayerPrefs.SetInt("CurrentSkin", skinIndex);
    }

    public void RefreshSkin()
    {
        instantiatedPlayer.GetComponent<SpriteRenderer>().sprite = SkinManager.instance.ShopItemList[PlayerPrefs.GetInt("CurrentSkin", 0)].image;
    }



    public void takeGoldenEnergy()
    {
        goldenEnergyPower = true;
        TakenSkillManager.instance.ShowSkill("GoldenEnergy");
        StartCoroutine(resetGoldenEnergy());
    }

    IEnumerator resetGoldenEnergy()
    {
        yield return new WaitForSeconds(SkillManager.instance.getCooldown("GoldenEnergy"));
        EffectManager.PlayAnim("DefaultEffect");
        goldenEnergyPower = false;
    }


    public void takeBlackHole()
    {
        TakenSkillManager.instance.ShowSkill("BlackHole");
    }



    public void takeTimeFreeze()
    {
        timeFreezePower = true;
        TakenSkillManager.instance.ShowSkill("TimeFreeze");
        StartCoroutine(resetTimeFreeze());
    }

    IEnumerator resetTimeFreeze()
    {
        yield return new WaitForSeconds(SkillManager.instance.getCooldown("TimeFreeze"));
        EffectManager.PlayAnim("DefaultEffect");
        timeFreezePower = false;
    }


    public void takeShield()
    {
        TakenSkillManager.instance.ShowSkill("Shield");
    }
}
