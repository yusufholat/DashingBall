using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    static Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public static void PlayAnim(string name)
    {
        animator.SetTrigger(name);
    }
}
