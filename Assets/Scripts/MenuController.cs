using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour {

	public GameObject title;
	public GameObject howToPlay;

	private int state;

	// Use this for initialization
	void Start () {
		state = 0;
	
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.anyKeyDown) {
			if (state == 0) {
				title.SetActive (false);
				howToPlay.SetActive (true);
				state = 1;
			} else {
				SceneManager.LoadScene ("circle_level_small");
			}
		}
	
	}
}
