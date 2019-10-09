using UnityEngine;
using System.Collections;

public class LightFlicker : MonoBehaviour 
{
	public float amount = 0.5f;
	public float rate = 1f;

	protected float baseIntensity;
	protected Light theLight;

	void Awake()
	{
		theLight = GetComponent<Light>();
		baseIntensity = theLight.intensity;
	}

	// Update is called once per frame
	void Update () 
	{
		theLight.intensity = baseIntensity - (amount * Mathf.PerlinNoise(0, Time.time * rate));
	}
}
