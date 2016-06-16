using UnityEngine;
using System.Collections;


public class Player : MonoBehaviour {


	// Player Rigid Body
	private Rigidbody2D mRigidbody;
	private Animator mAnimator;


	// Input axis
	private float mHorizontal;
	private float mVertical;

	// Self light
	public GameObject selfLight;

	// Physical parameters
	public float maxSpeed;
	public float acceleration;

	public float alphaRotation;

	// Current state
	public enum PlayerState { SQUARE, TRIANGLE, STAR, CIRCLE }
	private PlayerState mState;

	// Death Particle System
	public ParticleSystem deathParticles;
	// Death Audio System
	public AudioSource deathAudio;

	// Timer to keep form
	private float mTimer;
	public float maxTime;
	private float mCoolDown;

	public bool invisible;

	// Use this for initialization
	void Start () {
		mRigidbody = GetComponent<Rigidbody2D> ();
		mAnimator = GetComponent<Animator> ();
		mState = PlayerState.CIRCLE;
		mAnimator.SetInteger("State", 4);
		mTimer = maxTime;
		mCoolDown = maxTime;
		invisible = false;

	}
	
	// Update is called once per frame
	void Update () {
		mHorizontal = Input.GetAxis ("Horizontal");
		mVertical = Input.GetAxis ("Vertical");

		if (mTimer > maxTime) {
			if (mState != PlayerState.CIRCLE) {

				mState = PlayerState.CIRCLE;
				mAnimator.SetInteger("State", 4);
				mCoolDown = 0.0f;
			}

		}
		else {
			mTimer += Time.deltaTime;
		}

		if (mCoolDown < maxTime) {
			mCoolDown += Time.deltaTime;
		}
		else {


			// Change shape with each button
			if (Input.GetButtonDown("Fire1")) {
				if (mState == PlayerState.CIRCLE)
					mTimer = 0.0f;
				mState = PlayerState.SQUARE;
				mAnimator.SetInteger("State", 1);
			}
			if (Input.GetButtonDown("Fire2")) {
				if (mState == PlayerState.CIRCLE)
					mTimer = 0.0f;
				mState = PlayerState.TRIANGLE;
				mAnimator.SetInteger("State", 2);
			}
			if (Input.GetButtonDown("Fire3")) {
				if (mState == PlayerState.CIRCLE)
					mTimer = 0.0f;
				mState = PlayerState.STAR;
				mAnimator.SetInteger("State", 3);
			}
			if (Input.GetButtonDown("Fire4")) {
				mState = PlayerState.CIRCLE;
				mAnimator.SetInteger("State", 4);
				mCoolDown = maxTime - mTimer;
				mTimer = maxTime;
			}

		}
		/*
		if (Input.GetButtonDown("Fire4")) {
			mState = PlayerState.CIRCLE;
			mAnimator.SetInteger("State", 4);
		}*/
	}

	public PlayerState GetState () {
		return mState;
	}

	void FixedUpdate() {

		// Drag velocities
		mRigidbody.velocity = mRigidbody.velocity * 0.9f;
		mRigidbody.angularVelocity = mRigidbody.angularVelocity * 0.9f;

		// If velocity is not maximum and there is an input
		if (mRigidbody.velocity.sqrMagnitude < maxSpeed * maxSpeed &&
			(Mathf.Abs(mHorizontal) > float.Epsilon || Mathf.Abs(mVertical) > float.Epsilon)) {

			// Apply input force
			Vector2 force = new Vector2 (mHorizontal * acceleration, mVertical * acceleration);

			mRigidbody.AddForce (force);


			// Ajust rotation to point where it's going
			gameObject.transform.rotation = Quaternion.Slerp (transform.rotation,
				Quaternion.LookRotation (new Vector3(0.0f, 0.0f, 1.0f), new Vector3(force.x, force.y, 0.0f))
				, alphaRotation);

		}
		// Get light ahead of the body
		Vector2 scaledVelocity = mRigidbody.velocity / maxSpeed;
		selfLight.transform.position = gameObject.transform.position + new Vector3 (scaledVelocity.x, scaledVelocity.y, -1.0f) ;

		
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

		GameObject.FindGameObjectWithTag ("GameController").GetComponent<Portal> ().PlayerDead ();
	}
}
