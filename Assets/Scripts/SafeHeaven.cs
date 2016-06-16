using UnityEngine;
using System.Collections;

public class SafeHeaven : MonoBehaviour {

	private bool mActive;

	// Use this for initialization
	void Start () {
	
		mActive = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D coll) {
		if (coll.gameObject.CompareTag ("Player")) {
			coll.gameObject.GetComponent<Player> ().invisible = true;
			mActive = true;
		}

	}

	void OnTriggerExit2D(Collider2D coll) {
		if (coll.gameObject.CompareTag ("Player")) {
			coll.gameObject.GetComponent<Player> ().invisible = false;
			mActive = false;
		}
	}
}
