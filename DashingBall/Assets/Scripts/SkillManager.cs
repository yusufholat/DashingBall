using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public static SkillManager instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else Destroy(gameObject);
    }

    [System.Serializable]
    public class Skill
    {
        public string name;
        public Sprite sprite;
        public float cooldown;
    }

    public Skill[] skillList;


    public Sprite getSprite(string name)
    {
        Skill s = Array.Find(skillList, skill => skill.name == name);
        return s.sprite;
    }
    public float getCooldown(string name)
    {
        Skill s = Array.Find(skillList, skill => skill.name == name);
        return s.cooldown;
    }
}
