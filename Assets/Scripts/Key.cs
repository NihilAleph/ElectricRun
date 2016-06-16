using UnityEngine;
using System.Collections;

public class Key : MonoBehaviour {

	public float speed;

	public ParticleSystem deathParticles;
	public AudioSource deathAudio;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
		// Spin key
		gameObject.transform.Rotate (Vector3.forward * speed * Time.deltaTime);
	}

	void OnTriggerEnter2D(Collider2D col) {
		if (col.gameObject.CompareTag("Player")) {
			GameObject.FindGameObjectWithTag ("GameController").GetComponent<Portal> ().KeyDestroyed ();
			Instantiate (deathParticles, gameObject.transform.position, gameObject.transform.rotation);
			Instantiate (deathAudio, gameObject.transform.position, gameObject.transform.rotation);
			Destroy (gameObject);
		}
	}

}
