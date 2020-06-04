using UnityEngine;

public class TrackSelector : MonoBehaviour {
	public AudioClip clip; // this is the clip to be played on the MusicPlayer...
	public bool loop; // this will control if this clip should be loopable

	private void Start() {
		MusicPlayer.Play(this.clip, this.loop);
	}
}
