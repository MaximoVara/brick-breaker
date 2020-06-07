using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//  This forces the music player component to also have attached to it 
// an AudioSource component by deafault.
[RequireComponent(typeof(AudioSource))]
public class MusicPlayer : Singleton<MusicPlayer>
{
    private AudioSource current;
    private AudioSource next;
    private IEnumerator[] fader = new IEnumerator[2];
    
    private void Awake()
    {
        this.current = this.GetComponent<AudioSource>();

        var go = new GameObject("Next");
        go.transform.SetParent(this.transform);

        this.next = go.AddComponent<AudioSource>();
    }

    IEnumerator _doPlay(AudioClip clip, bool loop)
    {
        if (this.current.isPlaying == false)
        {
            this.current.clip = clip;
            this.current.loop = loop;
            this.current.Play();
            yield break; // forces the coroutine to terminate
        }
        
        // stop other instances coroutines
        foreach (IEnumerator i in fader)
        {
            if (i != null)
            {
                Instance.StopCoroutine(i);
            }
        }

        // Load the new clip to next for fading in. 
        this.next.clip = clip;
        this.next.loop = loop;
        this.next.Play();

        fader[0] = this._crossFade(this.current, this.current.volume, 0.0F, 1.5F);
        fader[1] = this._crossFade(this.next, 0.0F, 1.0F, 5.0F);

        // tell the current audio source to fade it's volume...
        StartCoroutine(fader[0]);
        // fade in the next clip and wait until this routine has finished...
        yield return StartCoroutine(fader[1]);

        // Once the next clip has fully faded in, swap the next to current...
        var temp = this.current;
        this.current = this.next;
        this.next = temp;

        // reset next 
        this.next.Stop();
        //this.next.clip = null;
        this.next.volume = 0.0F;
    }

    IEnumerator _crossFade(AudioSource source, float from, float to, float time)
    {
        var eTime = 0.0F; //ellapse time
        var progress = 0.0F; // progress

        while(progress <= 1.0F)
        {
            progress = eTime / time; // normilize the time...
            var volume = Mathf.Lerp(from, to, progress);
            source.volume = volume;
            eTime += Time.unscaledDeltaTime;
            yield return null;
        }
    }

    public static void Play(AudioClip clip){
        Play(clip, false);
    }

    public static void Play(AudioClip clip, bool loop){
        var audioSource = Instance.GetComponent<AudioSource>();
        /*if (audioSource.clip == clip)
        {
            return;
        }*/
        Instance.StartCoroutine(Instance._doPlay(clip, loop));
    }

}
