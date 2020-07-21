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

        if (!GameManager.gamePaused && Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject() ||
            !GameManager.gamePaused && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began && !EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
        {
            energyBarAnim.Play("dashanimation");
        }
    }

}
