using UnityEngine;
using System.Collections;

public class SmoothCamera : MonoBehaviour {

	private GameObject target;
	public float alpha;


	// Use this for initialization
	void Start () {
		// Set target as tag Player
		target = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		// Check if target still exists
		if (target != null) {
			// Follow target
			Vector3 targetPosition = new Vector3 (target.transform.position.x, target.transform.position.y, gameObject.transform.position.z);
			gameObject.transform.position = Vector3.Lerp (gameObject.transform.position, targetPosition, alpha);
		}
	}
}
