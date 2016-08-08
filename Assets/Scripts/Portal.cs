using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Portal : MonoBehaviour {

	// Particles when player enters Portal
	public ParticleSystem deathParticles;

	// Audio when player enters Portal
	public AudioSource PortalAudio;

    // Physical parameters
    public GlowingLight GlowingLight;
	public float SpinSpeed;
    private bool mActive = false;

	// Next scene when this one is done
	//public string nextScene;

	// Count how many keys destroyed
	//private int mKeysCount;
	// Initial key count
	//private int mKeys;


	// UI References
    /*
	public GameObject keysLeft;
	public GameObject key1;
	public GameObject key2;
	public GameObject key3;
	public GameObject key4;
	public GameObject key5;
	public GameObject ready;
	public GameObject go;
	public GameObject restart;
    */

	// Use this for initialization
	void Start () {
		//mKeysCount = 0;
        GlowingLight.SetGlowing(true);
        
		//mState = GameState.READY;
	}

    /*
	public GameState GetGameState() {
		return mState;
	}*/

	// Update is called once per frame
	void Update () {

		// Spin portal proportional to keyscount
		gameObject.transform.Rotate (Vector3.forward * SpinSpeed * Time.deltaTime);

        if (mActive)
        {

            GameObject player = GameObject.FindGameObjectWithTag("Player");
            player.transform.position = Vector3.Lerp(gameObject.transform.position, player.transform.position, 0.3f);
            player.transform.localScale = player.transform.localScale * 0.95f;
            gameObject.transform.localScale = gameObject.transform.localScale * 0.95f;
        }

	}

    /*
	IEnumerator FinishGame() {
		Destroy (GameObject.FindGameObjectWithTag ("Player"));
		float waitTime = 1.5f; 
		while (waitTime > 0.0f) {
			waitTime -= Time.deltaTime;
			yield return null;
		}
		// Start next scene
		SceneManager.LoadScene (nextScene);
	}
    */

	// Check when Player enters the portal
	void OnTriggerEnter2D(Collider2D col) {
		if (col.gameObject.CompareTag("Player")) {
            // Play portal sound and spin faster
			Instantiate (PortalAudio, gameObject.transform.position, gameObject.transform.rotation);
            SpinSpeed *= 50.0f;
            mActive = true;


        }
	}

    /*
	// Called when a player gets a key
	public void KeyDestroyed() {
		mKeysCount++;
		switch(mKeys - mKeysCount) {
		case 4:
			key5.SetActive (false);
			key4.SetActive (true);
			break;
		case 3:
			key4.SetActive (false);
			key3.SetActive (true);
			break;
		case 2:
			key3.SetActive (false);
			key2.SetActive (true);
			break;
		case 1:
			key2.SetActive (false);
			key1.SetActive (true);
			break;
		case 0:
			key1.SetActive (false);
			keysLeft.SetActive (false);
			break;
		}
		// Light portal proportional to keyscount
		selfLight.intensity = maxLight * mKeysCount / (mKeys);
	}
    */
}