using UnityEngine;
using System.Collections;

public class SmoothLight : MonoBehaviour {

    // Light target in local coordinates
    private Vector3 mPositionTarget;
    public float PositionAlpha = 0.2f;

    // Light component
    private Light mLight;

    // Light intensity target
    private float mIntensityTarget;
    public float IntensityAlpha = 0.2f;

    public float InitialIntensity = 2.0f;

    // Use this for initialization
    void Start () {
        mPositionTarget = gameObject.transform.localPosition;

        mLight = GetComponent<Light>();
        mIntensityTarget = InitialIntensity;

    }
	
	// Update is called once per frame
	void Update () {
        // Move Light smoothly to the target
        gameObject.transform.localPosition = Vector3.Lerp(gameObject.transform.localPosition, mPositionTarget, PositionAlpha);
        mLight.intensity += (mIntensityTarget - mLight.intensity) * IntensityAlpha;
    }

    // Set target in local coordinates
    public void SetLocalTarget(Vector3 target)
    {
        mPositionTarget = target;
    }

    // Set target in local coordinates
    public void SetIntensityTarget(float intensity)
    {
        mIntensityTarget = intensity;
    }
}
