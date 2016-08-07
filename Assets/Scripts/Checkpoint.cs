using UnityEngine;
using System.Collections;

public class Checkpoint : MonoBehaviour {
    
    public GlowingLight GlowingLight;
    public float SpinSpeed;
    public float HelixSpinSpeed;

    private bool mActive = false;
    

    // Use this for initialization
    void Start () {

    }
	
	// Update is called once per frame
	void Update ()
    {
        // Spin checkpoint
        gameObject.transform.Rotate(Vector3.forward * Time.deltaTime * SpinSpeed);


        if (mActive)
        {

            foreach (Transform child in transform)
            {
                child.transform.Rotate(Vector3.forward * Time.deltaTime * HelixSpinSpeed);

            }
        }
    }



    // Check if it's in border of a heaven
    void OnTriggerStay2D(Collider2D coll)
    {
        Debug.Log("enter");
        // If exiting safe heaven border
        if (coll.gameObject.CompareTag("Player"))
        {
            // then it's not safe
            //Debug.Log("Not Safe");
            
            //coll.gameObject.GetComponent<Player>().SetSafe(false);
            GlowingLight.SetGlowing(true);


            mActive = true;
            
        }
    }
}
