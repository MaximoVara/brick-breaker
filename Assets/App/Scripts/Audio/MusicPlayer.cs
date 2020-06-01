using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//  This forces the music player component to also have attached to it 
// an AudioSource component by deafault.
[RequireComponent(typeof(AudioSource))]
public class MusicPlayer : Singleton<MusicPlayer>
{
    public static void Play(AudioClip clip){
        MusicPlayer.Play(clip, false);
    }
    public static void Play(AudioClip clip, bool loop){
        var audioSource = Instance.GetComponent<AudioSource>();

        if(audioSource.clip == clip) return;
        audioSource.clip = clip;
        audioSource.loop = loop;
        
        audioSource.Play();
    }
}
