using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource[] AudioSources;
    [SerializeField] AudioClip[] BgAudios;
    
    public static AudioManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }

        //Application.targetFrameRate = 60;
    }

    private void OnEnable()
    {
        PuzzleManager.OnRounEnd += StopAudios;
    }

    private void OnDisable()
    {
        PuzzleManager.OnRounEnd -= StopAudios;
    }
    private void Start()
    {

        int random = UnityEngine.Random.Range(0, BgAudios.Count());
        PlaySound("BG", BgAudios[random], true);
    }
    private void OnRoundStarted()
    {
        AudioSources[AudioSources.Length-1].Play();
    }

    private void FinishLineCross()
    {
        AudioSources[AudioSources.Length-1].Stop();
    }

    public void PlaySound(string SoundType, AudioClip audioClip,bool Looping)
    {
        if (audioClip==null)
        {
            return;
        }
        
        if (SoundType=="Player")
        {
            AudioSources[0].clip = audioClip;
            AudioSources[0].loop = Looping;
            AudioSources[0].Play();
        }
        else if (SoundType=="Prop")
        {
            AudioSources[1].clip = audioClip;
            AudioSources[1].loop = Looping;
            AudioSources[1].Play();
        }
        else if (SoundType=="Police")
        {
            AudioSources[2].clip = audioClip;
            AudioSources[2].loop = Looping;
            AudioSources[2].Play();
        }
        else if (SoundType == "Spare")
        {
            AudioSources[3].clip = audioClip;
            AudioSources[3].loop = Looping;
            AudioSources[3].Play();
        }
        else if (SoundType == "BG")
        {
            AudioSources[4].clip = audioClip;
            AudioSources[4].loop = Looping;
            AudioSources[4].Play();
        }

    }
    public void StopAudios()
    {
        foreach (AudioSource ads in AudioSources)
        {
            if (ads == AudioSources[AudioSources.Count() - 1]) return;
            ads.Stop();
        }
    }
}
