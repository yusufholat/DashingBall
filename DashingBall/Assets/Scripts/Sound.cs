using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

[System.Serializable]
public class Sound
{
    public AudioClip clip;

    [Range(0f, 1f)]
    public float volume;
    [Range(0f, 1f)]
    public float pitch;

    [HideInInspector]
    public AudioSource source;
}
