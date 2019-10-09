using UnityEngine;
using System.Collections;

public class LightFlasher : MonoBehaviour 
{
	public float flashInterval = 1f;
	public float flashLength = 0.1f;

	protected Light theLight;
	protected float intervalTime = 0;
	protected float flashTime = 0;

	void Awake()
	{
		theLight = GetComponent<Light>();
	}

	// Update is called once per frame
	void Update () 
	{
		intervalTime += Time.deltaTime;

		if (theLight.enabled)
		{
			flashTime += Time.deltaTime;

			if (flashTime >= flashLength)
			{
				flashTime -= flashLength;
				theLight.enabled = false;
			}
		}

		if (intervalTime >= flashInterval)
		{
			theLight.enabled = true;
			intervalTime -= flashInterval;
		}
	}
}
