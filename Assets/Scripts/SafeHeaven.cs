using UnityEngine;
using System.Collections;

public class SafeHeaven : MonoBehaviour {
    
    public float SqrRange = 4.0f;
    public GlowingLight GlowingLight;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

    // Check if it's in border of a heaven
    void OnTriggerStay2D(Collider2D coll)
    {
        Debug.Log("enter");
        // If exiting safe heaven border
        if (coll.gameObject.CompareTag("Player"))
        {
            // check if it's mostly outside the heaven
            if ((coll.gameObject.transform.position - transform.position).sqrMagnitude > SqrRange)
            {
                // then it's not safe
                //Debug.Log("Not Safe");
                coll.gameObject.GetComponent<Player>().SetSafe(false);
                GlowingLight.SetGlowing(false);


            }
            else
            {
                // or else it's safe
                //Debug.Log("Safe");
                coll.gameObject.GetComponent<Player>().SetSafe(true);
                GlowingLight.SetGlowing(true);

            }
        }
    }
}
