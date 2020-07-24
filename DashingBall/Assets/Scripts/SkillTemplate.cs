using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SkillTemplate : MonoBehaviour
{
    float cooldown;
    Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
        cooldown = float.Parse(transform.GetChild(1).GetComponent<TextMeshProUGUI>().text);
    }

    void Update()
    {
        if (cooldown >= 0)
        transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = cooldown.ToString("0.0");
        else
        {
            anim.SetTrigger("end");
            EffectManager.PlayAnim("DefaultEffect");
            Destroy(gameObject, 1f);
        }

        cooldown -= Time.deltaTime;
    }
}
