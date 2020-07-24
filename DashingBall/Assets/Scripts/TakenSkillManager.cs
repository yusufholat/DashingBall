using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TakenSkillManager : MonoBehaviour
{

    public static TakenSkillManager instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else Destroy(gameObject);
    }

    public GameObject skillTemplate;
    public Transform skillViewContent;

    GameObject goldenEnergy;
    GameObject blackHole;

    public void ShowSkill(string name)
    {
        if (name == "GoldenEnergy") {
            goldenEnergy = Instantiate(skillTemplate, skillViewContent);
            goldenEnergy.transform.GetChild(0).GetComponent<Image>().sprite = SkillManager.instance.getSprite("GoldenEnergy");
            goldenEnergy.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = SkillManager.instance.getCooldown("GoldenEnergy").ToString();
        }
        else if (name == "BlackHole") {
            blackHole = Instantiate(skillTemplate, skillViewContent);
            blackHole.transform.GetChild(0).GetComponent<Image>().sprite = SkillManager.instance.getSprite("BlackHole");
            blackHole.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = SkillManager.instance.getCooldown("BlackHole").ToString();
        }            
    }

}
