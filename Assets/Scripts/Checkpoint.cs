using UnityEngine;
using System.Collections;

public class Checkpoint : MonoBehaviour {
    
    public GlowingLight GlowingLight;
    public float SpinSpeed;
    public float HelixSpinSpeed;

    private bool mActive = false;
    // Active getter
    public bool IsActive { get { return mActive; } }

    // Use this for initialization
    void Start () {

    }
	
	// Update is called once per frame
	void Update ()
    {
        // Spin checkpoint
        gameObject.transform.Rotate(Vector3.forward * Time.deltaTime * SpinSpeed);


        // If this check point is active, rotate helixes
        if (mActive)
        {

            foreach (Transform child in transform)
            {
                child.transform.Rotate(Vector3.forward * Time.deltaTime * HelixSpinSpeed);

            }
        }
    }



    // Check if player entered checkpoint
    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("Player"))
        { 
            // If checkpoint wasn't active, activate now
            if (!mActive)
            {
                // First deactivate all checkpoints
                GameController.Instance.SetCheckpoint(this);

                // Start Light
                GlowingLight.SetGlowing(true);
                // Activate this checkpoint
                mActive = true;
            }

        }
    }

    public void Deactivate()
    {
        mActive = false;
        GlowingLight.SetGlowing(false);
    }
}
