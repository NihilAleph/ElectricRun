using UnityEngine;
using System.Collections;

public class InputKeyDetector : MonoBehaviour {

    public GlowingLight SelfLight;
    public string KeyName;
    private bool mActive = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (!mActive)
        {

            if (Input.GetKeyDown(KeyName))
            {
                SelfLight.SetGlowing(true);
                mActive = true;
            }
        }
	}
}
