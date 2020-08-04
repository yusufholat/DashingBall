using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EnergyBarController : MonoBehaviour
{
    EnergyBar energyBar;
    Animator energyBarAnim;
    void Awake()
    {
        energyBarAnim = GetComponent<Animator>();
        energyBar = GetComponent<EnergyBar>();
    }

    void Start()
    {
        energyBar.SetMaxHealth(PlayerManager.maxHealth);
    }

    void Update()
    {
        energyBar.SetHealth(PlayerManager.currentHealth);

        if (Input.GetMouseButtonDown(0) && !GameManager.IsPointerOverUIObject() ||
            Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began && !GameManager.IsPointerOverUIObject())
        {
            energyBarAnim.Play("dashanimation");
        }
    }

}
