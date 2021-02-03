using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    // Start is called before the first frame update
    private void Awake()
    {
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;

        }
    }
    void Start()
    {
        Play("TribalTheme");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Play(string audioName)
    {
        Sound s = Array.Find(sounds, sound => sound.name == audioName);
        s.source.Play();
    }

    public void Switch(string currentAudio, string nextAudio)
    {
        Sound s = Array.Find(sounds, sound => sound.name == currentAudio);
        s.source.Stop();

        Sound n = Array.Find(sounds, sound => sound.name == nextAudio);
        n.source.Play();

    }
}
