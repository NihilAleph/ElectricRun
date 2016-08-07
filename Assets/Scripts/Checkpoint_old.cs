using UnityEngine;
using System.Collections;

public class Checkpoint_old : MonoBehaviour
{

    public GlowingLight GlowingLight;
    public float SpinSpeed;

    private Animator mAnimator;

    // Use this for initialization
    void Start()
    {
        mAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // Spin checkpoint
        gameObject.transform.Rotate(Vector3.forward * Time.deltaTime * SpinSpeed);


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
            coll.gameObject.GetComponent<Player>().SetSafe(false);
            GlowingLight.SetGlowing(true);


            mAnimator.SetBool("Active", true);
        }
    }
}
