using UnityEngine;
using System.Collections;

public class IdleShape : ShapeAgent {
    

	// Player reference
	private Player mPlayer;
    public float FoV;


	// Use this for initialization
	override protected void Start () {
        base.Start();
		mPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
	}

    // Update is called once per frame
    override protected void Update ()
    {
        base.Update();
    }

    override protected void FixedUpdate() {
        base.FixedUpdate();
		// Apply force if velocity is not maximum
		if (pRigidbody.velocity.sqrMagnitude < MaxSpeed * MaxSpeed) {
			Vector3 force = new Vector3 (0.0f, 0.0f, 0.0f);
            
			// Check if user still exists
			if (mPlayer != null && (mPlayer.transform.position - transform.position).sqrMagnitude < FoV * FoV 
				&& !mPlayer.GetComponent<Player>().safe) {
				// Force by eletrical charge with player
				// If same state, gets attracted, else repels (which is stronger)
				if (mPlayer.CurrentState != CurrentState) {
					float aForce = AttractionForce;
                    // If the player is in circle form, attraction is less powerful
					if (mPlayer.CurrentState ==  ShapeForm.CIRCLE)
						aForce /= 2.0f;
					force -= (gameObject.transform.position - mPlayer.transform.position).normalized
						* (aForce/ (gameObject.transform.position - mPlayer.transform.position).sqrMagnitude);
				}
				else {

					force += (gameObject.transform.position - mPlayer.transform.position).normalized
					* (RepelForce / (gameObject.transform.position - mPlayer.transform.position).sqrMagnitude);
				}

                // Apply force
				pRigidbody.AddForce (new Vector2 (force.x, force.y));

                // Set light intensity proportional to velocity
                // Make a better equation
                // SelfLight.SetIntensityTarget(pRigidbody.velocity.sqrMagnitude / (MaxSpeed * MaxSpeed / 8) * SelfLight.InitialIntensity);

			}
		}
        
	}

	void OnCollisionEnter2D(Collision2D col) {
		Die ();
	}

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("Arena"))
        {
            Die();
        }
    }


}
