using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeFreezeArea : MonoBehaviour
{
    void Start()
    {
        float cooldown = ItemManager.instance.getCooldown("TimeFreeze");
        var ps = GetComponent<ParticleSystem>();

        ps.Stop();
        var main = ps.main;
        main.duration = cooldown - 2f;
        ps.Play();
    }

}
