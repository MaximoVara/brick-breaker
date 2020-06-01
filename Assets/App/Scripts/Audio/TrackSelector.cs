using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackSelector : MonoBehaviour
{
    public AudioClip clip;
    public bool loop;
    void Start()
    {
        MusicPlayer.Play(this.clip, this.loop);
    }

}
