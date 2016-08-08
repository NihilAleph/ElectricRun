using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System;

public class GameController : MonoBehaviour {

    //Static instance of GameManager which allows it to be accessed by any other script.
    public static GameController Instance = null;

    // List of checkpoints in the arena
    private Vector3 mCheckpointPosition;

    public Player PlayerPrefab;
    public SmoothCamera Camera;
     
    // GameState
    public enum GameState { LOADING, READY, PLAYING, GAMEOVER, SUCCESS, FINISHED }
    private GameState mState;

    //Awake is always called before any Start functions
    void Awake()
    {
        mState = GameState.PLAYING;
        //Check if instance already exists
        if (Instance == null)

            //if not, set instance to this
            Instance = this;

        //If instance already exists and it's not this:
        else if (Instance != this)

            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);

        //Sets this to not be destroyed when reloading scene
        DontDestroyOnLoad(gameObject);
        
    }

    // Use this for initialization
    void Start () {
        // Get all checkpoints in stage

        Debug.Log("Start");
    }
	
	// Update is called once per frame
	void Update () {

        switch (mState)
        {
            case GameState.LOADING:
                mState = GameState.PLAYING;
                GameObject player = GameObject.FindGameObjectWithTag("Player");
                
                player.transform.position = mCheckpointPosition;
                break;
            case GameState.GAMEOVER:
                if (Input.anyKeyDown)
                {

                    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                    mState = GameState.LOADING;
                }
                break;


            // On game Finished, Portal sucks player and dissappear
            case GameState.SUCCESS:
                /*
                if (player.transform.localScale.sqrMagnitude < 0.2f)
                {
                    mState = GameState.FINISHED;
                    //Instantiate (deathParticles, gameObject.transform.position, gameObject.transform.rotation);
                    //Instantiate (deathParticles, gameObject.transform.position, gameObject.transform.rotation);
                    //StartCoroutine (FinishGame());
                    
                }*/
                break;
            case GameState.FINISHED:
                break;
            default:
                break;

        }
    }

    public void SetCheckpoint(Checkpoint checkpoint)
    {

        foreach (GameObject c in GameObject.FindGameObjectsWithTag("Checkpoint"))
        {
            c.GetComponent<Checkpoint>().Deactivate();
        }
        mCheckpointPosition = new Vector3(checkpoint.transform.position.x, checkpoint.transform.position.y, checkpoint.transform.position.z);
        
    }

    // Called when player dies
    public void PlayerDead()
    {
        mState = GameState.GAMEOVER;
    }
}
