using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EnergyBarController : MonoBehaviour
{
    public Player player;
    EnergyBar energyBar;
    Animator energyBarAnim;
    void Awake()
    {
        energyBarAnim = GetComponent<Animator>();
        energyBar = GetComponent<EnergyBar>();
    }

    void Start()
    {
        energyBar.SetMaxHealth(player.maxHealth);
    }

    void Update()
    {
        energyBar.SetHealth(player.currentHealth);

        if (!UIManager.gameIsPaused && Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject() ||
            !UIManager.gameIsPaused && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began && !EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
        {
            energyBarAnim.Play("dashanimation");
        }
    }

}
