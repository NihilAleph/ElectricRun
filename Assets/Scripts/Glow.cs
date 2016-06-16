using UnityEngine;
using System.Collections;

public class Glow : MonoBehaviour {
	public Light glowingLight;
	// Time counter for fluctuation
	private double mTime;

	// Frequency of the light
	public float period;
	public float intensityDelta;

	// Particles emmited when something touches
	public ParticleSystem deathParticles;

	// Use this for initialization
	void Start () {
		mTime = 0;
	}
	
	// Update is called once per frame
	void Update () {
		mTime += Time.deltaTime;

		glowingLight.intensity = 1.0f + Mathf.Sin ((float)mTime / period * 2 * Mathf.PI) * intensityDelta;

	}

	void OnTriggerEnter2D(Collider2D coll) {
		Instantiate (deathParticles, coll.gameObject.transform.position, gameObject.transform.rotation);
	}

}
