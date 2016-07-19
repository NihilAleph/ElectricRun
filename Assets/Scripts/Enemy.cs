using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	// Enemy Rigid Body
	private Rigidbody2D mRigidbody;

	// Input axis
	private float mHorizontal;
	private float mVertical;

	// Self Light
	public GameObject selfLight;

	// Physical parameter
	public float maxSpeed;
	public float attractionForce;
	public float repelForce;
	public float alphaRotation;

	// Player reference
	private GameObject player;

	// Death Particle System
	public ParticleSystem deathParticles;
	// Death Audio System
	public AudioSource deathAudio;


	// Use this for initialization
	void Start () {

		mRigidbody = GetComponent<Rigidbody2D> ();
		player = GameObject.FindGameObjectWithTag("Player");
	}

	// Update is called once per frame
	void Update () {
	}

	void FixedUpdate() {
		// Drag velocities
		mRigidbody.velocity = mRigidbody.velocity * 0.9f;
		mRigidbody.angularVelocity = mRigidbody.angularVelocity * 0.9f;

		// Apply force if velocity is not maximum
		if (mRigidbody.velocity.sqrMagnitude < maxSpeed * maxSpeed) {
			Vector3 force = new Vector3 (0.0f, 0.0f, 0.0f);

			// Find target type
			Player.PlayerState target = Player.PlayerState.SQUARE;
			switch (gameObject.tag) {
			case "Square":
				target = Player.PlayerState.SQUARE;
				break;
			case "Triangle":
				target = Player.PlayerState.TRIANGLE;
				break;
			case "Star":
				target = Player.PlayerState.STAR;
				break;
			default:
				target = Player.PlayerState.SQUARE;
				break;
			}
			// Check if user still exists
			if (player != null && (player.transform.position - transform.position).sqrMagnitude < 625.0f 
				&& !player.GetComponent<Player>().safe) {
				// Force by eletrical charge with player
				// If same state, gets attracted, else repels (which is stronger)
				if ((player.GetComponent<Player> ()).GetState () != target) {
					float aForce = attractionForce;
					if ((player.GetComponent<Player> ()).GetState () == Player.PlayerState.CIRCLE)
						aForce /= 2.0f;
					force -= (gameObject.transform.position - player.transform.position).normalized
						* (aForce/ (gameObject.transform.position - player.transform.position).sqrMagnitude);
				}
				else {

					force += (gameObject.transform.position - player.transform.position).normalized
					* (repelForce / (gameObject.transform.position - player.transform.position).sqrMagnitude);
				}

				mRigidbody.AddForce (new Vector2 (force.x, force.y));

				// Ajust rotation to point where it's going
				gameObject.transform.rotation = Quaternion.Slerp (transform.rotation,
					Quaternion.LookRotation (new Vector3 (0.0f, 0.0f, 1.0f), new Vector3 (force.x, force.y, 0.0f))
				, alphaRotation);

			}
		}

		// Get light ahead of the body
		Vector2 scaledVelocity = mRigidbody.velocity / maxSpeed;
		selfLight.transform.position = gameObject.transform.position + (new Vector3 (scaledVelocity.x, scaledVelocity.y, -1.0f)) ;
	}

	void OnCollisionEnter2D(Collision2D col) {
		Die ();
	}

	void OnTriggerExit2D(Collider2D coll) {
		if (coll.gameObject.CompareTag ("Arena")) {
			Die ();
		}
	}

	void Die() {

		Instantiate (deathParticles, gameObject.transform.position, gameObject.transform.rotation);
		Instantiate (deathAudio, gameObject.transform.position, gameObject.transform.rotation);
		Destroy (gameObject);
	}

}
