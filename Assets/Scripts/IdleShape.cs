using UnityEngine;
using System.Collections;

public class IdleShape : ShapeAgent {
    

	// Player reference
	private GameObject mPlayer;


	// Use this for initialization
	override protected void Start () {
        base.Start();
		mPlayer = GameObject.FindGameObjectWithTag("Player");
	}

    // Update is called once per frame
    override protected void Update () {
	}

    override protected void FixedUpdate() {
        /*
		// Apply force if velocity is not maximum
		if (pRigidbody.velocity.sqrMagnitude < MaxSpeed * MaxSpeed) {
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
			if (mPlayer != null && (mPlayer.transform.position - transform.position).sqrMagnitude < 625.0f 
				&& !mPlayer.GetComponent<Player>().safe) {
				// Force by eletrical charge with player
				// If same state, gets attracted, else repels (which is stronger)
				if ((mPlayer.GetComponent<Player> ()).GetState () != target) {
					float aForce = AttractionForce;
					if ((mPlayer.GetComponent<Player> ()).GetState () == Player.PlayerState.CIRCLE)
						aForce /= 2.0f;
					force -= (gameObject.transform.position - mPlayer.transform.position).normalized
						* (aForce/ (gameObject.transform.position - mPlayer.transform.position).sqrMagnitude);
				}
				else {

					force += (gameObject.transform.position - mPlayer.transform.position).normalized
					* (RepelForce / (gameObject.transform.position - mPlayer.transform.position).sqrMagnitude);
				}

				pRigidbody.AddForce (new Vector2 (force.x, force.y));
                

			}
		}

		// Get light ahead of the body
		Vector2 scaledVelocity = pRigidbody.velocity / MaxSpeed;
		SelfLight.transform.position = gameObject.transform.position + (new Vector3 (scaledVelocity.x, scaledVelocity.y, -1.0f)) ;
        */
	}

	void OnCollisionEnter2D(Collision2D col) {
		Die ();
	}
    

}
