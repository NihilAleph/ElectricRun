using UnityEngine;
using System.Collections;

public class GlowingLight : MonoBehaviour {
	private Light mLight;
	// Time counter for fluctuation
	private double mTime;

	// Frequency of the light
	public float period;
	public float intensityDelta;
    public float initialIntensity = 1.0f;

    // Glowing or not
    public bool glowing = false;

	// Particles emmited when something touches
	//public ParticleSystem deathParticles;

	// Use this for initialization
	void Start () {
        mLight = GetComponent<Light>();
        // Initiate time in a vale
		mTime = -period/4.0f;
	}
	
	// Update is called once per frame
	void Update () {
        // only glows if set to do so
        if (glowing)
        {

            mTime += Time.deltaTime;

            mLight.intensity = initialIntensity + Mathf.Sin((float)mTime / period * 2 * Mathf.PI) * intensityDelta;
        }

	}

    // initiate or stop the glowing
    public void SetGlowing(bool set)
    {
        if (!set && glowing)
        {
            // Turn off the light
            mTime = -period / 4.0f;
            mLight.intensity = initialIntensity - intensityDelta;
        }
        glowing = set;
    }

    /*
	void OnTriggerEnter2D(Collider2D coll) {
		Instantiate (deathParticles, coll.gameObject.transform.position, gameObject.transform.rotation);
	}
    */
}
