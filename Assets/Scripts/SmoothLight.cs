using UnityEngine;
using System.Collections;

public class SmoothLight : MonoBehaviour {

    // Light target in local coordinates
    private Vector3 mTarget;
    public float Alpha;

	// Use this for initialization
	void Start () {
        mTarget = new Vector3(0.0f, 0.0f, 0.0f);

    }
	
	// Update is called once per frame
	void Update () {
        // Move Light smoothly to the target
        gameObject.transform.localPosition = Vector3.Lerp(gameObject.transform.localPosition, mTarget, Alpha); 
	}

    // Set target in local coordinates
    public void SetLocalTarget(Vector3 target)
    {
        mTarget = target;
    }
}
