using UnityEngine;
using System.Collections;

public class ShapeAgent : MonoBehaviour
{

    // Agent rigid body
    protected Rigidbody2D pRigidbody;

    // Physical parameter
    public float MaxSpeed;
    public float TerminalSpeed;
    public float TerminalAngularSpeed;
    public float AttractionForce;
    public float RepelForce;
    public float AlphaRotation;


    // All possible Shape State
    public enum ShapeForm { SQUARE, TRIANGLE, HEXAGON, CIRCLE }
    public ShapeForm CurrentState;


    // Self Light
    public SmoothLight SelfLight;

    // Death Particle System
    public ParticleSystem DeathParticles;
    // Death Audio System
    public AudioSource DeathAudio;


    // Use this for initialization
    protected virtual void Start ()
    {
        pRigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    protected virtual void Update ()
    {

    }

    protected virtual void FixedUpdate()
    {
        // Drag velocity if above terminal velocity
        if (pRigidbody.velocity.sqrMagnitude > TerminalSpeed * TerminalSpeed)
        {
            pRigidbody.velocity = pRigidbody.velocity * 0.9f;
        }
        if (pRigidbody.angularVelocity > TerminalAngularSpeed)
        {
            pRigidbody.angularVelocity = pRigidbody.angularVelocity * 0.9f;
        }


        // Ajust rotation to point where it's going
        // Only do rotational adjustment if there is a velocity
        if (pRigidbody.velocity.sqrMagnitude > float.Epsilon)
        {
            gameObject.transform.rotation = Quaternion.Slerp(transform.rotation,
                Quaternion.LookRotation(new Vector3(0.0f, 0.0f, 1.0f), new Vector3(pRigidbody.velocity.x, pRigidbody.velocity.y, 0.0f))
            , AlphaRotation);
        }

        // Adjust light to be ahead of the body when moving
        float scaledVelocity = pRigidbody.velocity.sqrMagnitude / MaxSpeed / MaxSpeed;
        Vector3 target = new Vector3(0.0f, scaledVelocity, -1.0f);
        // Center correction for triangle
        if (CurrentState == ShapeForm.TRIANGLE)
        {
            target -= new Vector3(0.0f, 0.2f, 0.0f);
        }
        SelfLight.SetLocalTarget(target);
    }


    protected virtual void Die()
    {
        // When shape die, send particles, play audio and destroy the game object
        Instantiate(DeathParticles, gameObject.transform.position, gameObject.transform.rotation);
        Instantiate(DeathAudio, gameObject.transform.position, gameObject.transform.rotation);
        Destroy(gameObject);
    }
}
