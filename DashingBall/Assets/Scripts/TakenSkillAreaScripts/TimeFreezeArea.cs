using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeFreezeArea : MonoBehaviour
{
    private void Awake()
    {
        float cooldown = ItemManager.instance.getCooldown("TimeFreeze");
        var main = GetComponent<ParticleSystem>().main;
        main.duration = cooldown - 2f; // last particle effect lifetime
    }

}
