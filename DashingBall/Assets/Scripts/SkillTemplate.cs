using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillTemplate : MonoBehaviour
{
    float cooldown;
    Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
        cooldown = transform.GetChild(1).GetComponent<Slider>().maxValue;
    }

    void Update()
    {
        if (cooldown >= 0)
        {
            transform.GetChild(1).GetComponent<Slider>().value = cooldown;
        }        
        else
        {
            anim.SetTrigger("end");
            Destroy(gameObject, 1f);
        }

        cooldown -= Time.deltaTime;
    }
}
