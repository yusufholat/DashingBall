using UnityEngine.Audio;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    static AudioManager instance;
    private void Awake()
    {

        if (instance == null)
            instance = this;
        else Destroy(gameObject);

        DontDestroyOnLoad(gameObject);

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.playOnAwake = s.playOnAwake;
            s.source.volume = s.volume;
        }
       
    }
    private void Start()
    {
        //Play("MenuMusic");
    }

    public void Play(string name)
    {

        Sound s = Array.Find(sounds, sound => sound.name == name);

        if (s == null) return;
        s.source.Play();
    }

    public void Pause(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);

        if (s == null) return;
        s.source.Pause();
    }

    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);

        if (s == null) return;
        s.source.Stop();
        
    }
    public bool IsPlaying(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);

        if (s == null) return false;
        return s.source.isPlaying;
    }
}
