using UnityEngine;
using System.Collections;


public class Player : ShapeAgent {


	// Player Rigid Body
	private Animator mAnimator;


	// Input axis
	private float mHorizontal;
	private float mVertical;
    
	public float acceleration;
    
    
	public bool safe;

	// Use this for initialization
	protected override void Start ()
    {
        base.Start();
        pRigidbody = GetComponent<Rigidbody2D> ();
		mAnimator = GetComponent<Animator> ();
		CurrentState = ShapeForm.CIRCLE;
		mAnimator.SetInteger("State", 4);
        
        
		safe = false;

	}
	
	// Update is called once per frame
	protected override void Update ()
    {
        base.Update();


        mHorizontal = Input.GetAxis ("Horizontal");
		mVertical = Input.GetAxis ("Vertical");

        // Change shape with each button
        if (Input.GetButtonDown("Fire1")) {
            CurrentState = ShapeForm.SQUARE;
			mAnimator.SetInteger("State", 1);
		}
		if (Input.GetButtonDown("Fire2")) {
            CurrentState = ShapeForm.TRIANGLE;
			mAnimator.SetInteger("State", 2);
		}
		if (Input.GetButtonDown("Fire3")) {
            CurrentState = ShapeForm.HEXAGON;
			mAnimator.SetInteger("State", 3);
		}
		if (Input.GetButtonDown("Fire4")) {
			CurrentState = ShapeForm.CIRCLE;
			mAnimator.SetInteger("State", 4);
		}
	}

	protected override void FixedUpdate() {

        base.FixedUpdate();
        // If velocity is not maximum and there is an input
        if (pRigidbody.velocity.sqrMagnitude < MaxSpeed * MaxSpeed &&
			(Mathf.Abs(mHorizontal) > float.Epsilon || Mathf.Abs(mVertical) > float.Epsilon)) {

			// Apply input force
			Vector2 force = new Vector2 (mHorizontal * acceleration, mVertical * acceleration);

			pRigidbody.AddForce (force);
            
		}


    }

    // Setting if player is safe or not
    public void SetSafe (bool set)
    {
        safe = set;
        /*
        if (set)
        {
            // If it is becoming safe, turn off the light
            if (!this.safe)
            {
                selfLight.GetComponent<Light>().intensity = 0.35f;
                this.safe = true;

            }
        }
        else
        {
            // If it is not safe anymore, turn on the light
            if (this.safe)
            {
                selfLight.GetComponent<Light>().intensity = 2.0f;
                this.safe = false;
            }
        }
        */
    }


	void OnCollisionEnter2D(Collision2D col) {
		Die ();

	}


	void OnTriggerEnter2D(Collider2D coll) {
		if (coll.gameObject.CompareTag ("Arena")) {
			Die ();
		}
	}

    protected override void Die()
    {
        base.Die();
        GameController.Instance.PlayerDead();
    }
}
