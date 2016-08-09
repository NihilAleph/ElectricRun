using UnityEngine;
using System.Collections;

public class ParticleSystemAutoDestroy : MonoBehaviour {

	private ParticleSystem mParticleSystem;

	// Use this for initialization
	void Start () {
		mParticleSystem = GetComponent<ParticleSystem> ();
	}
	
	// Update is called once per frame
	void Update () {

		if (!mParticleSystem.IsAlive()) {
			Destroy (gameObject);
		}
	}
}
