using UnityEngine;
using System.Collections;

public class AudioSourceAutoDestroy : MonoBehaviour {

	private AudioSource mAudioSource;

	// Use this for initialization
	void Start () {
		mAudioSource = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (!mAudioSource.isPlaying) {
			Destroy (gameObject);
		}
	
	}
}
