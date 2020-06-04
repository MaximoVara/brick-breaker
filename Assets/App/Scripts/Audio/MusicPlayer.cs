using UnityEngine;

// This forces the MusicPlayer component to also have attached to it
// an AudioSource component.
[RequireComponent(typeof(AudioSource))]
public class MusicPlayer : Singleton<MusicPlayer> {
	public static void Play(AudioClip clip) {
		// Play click, with looping set to false...
		MusicPlayer.Play(clip, false);
	}

	public static void Play(AudioClip clip, bool loop) {
		var audioSource = Instance.GetComponent<AudioSource>();

		if(audioSource.clip == clip) return;

		audioSource.clip = clip;
		audioSource.loop = loop;

		audioSource.Play();
	}
}