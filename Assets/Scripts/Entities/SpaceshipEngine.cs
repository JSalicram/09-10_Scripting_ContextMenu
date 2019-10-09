using UnityEngine;
using System.Collections;

// Represents a spaceship's engine
public class SpaceshipEngine : MonoBehaviour 
{
	public float maxThrust = 20f;			// Maximum amount of thrust the engine can output at full throttle (1.0)
	public float maxExhaust = 100f;			// Max number of particles to be emitted as exhaust each frame.
	public ParticleSystem particles;		// Particle system for the engine
	public Light engineLight;				// The light that represents the light given off by the exhaust
	public float engineFadeTime;			// The number of seconds it takes for the engine light to fade out
	public float engineFlickerAmount = 0.5f;// The amount the engine light intensity should flicker
	public float engineFlickerRate = 1f;	// The rate at which the engine light should flicker

	protected float engineLightIntensity;	// The original intensity of the engine light
	protected float throttle = 0;			// Represents a percentage of maxThrust (0-1)
	protected Rigidbody2D affectedBody;		// The body to which force should be applied
	protected AudioSource sfx;				// A reference to any attached sound effect
	
	void Awake()
	{
		sfx = GetComponent<AudioSource>();
		if (engineLight != null)
			engineLightIntensity = engineLight.intensity;
	}

	public void SetAffectedBody(Rigidbody2D rb2D)
	{
		affectedBody = rb2D;
	}

	public float Throttle
	{
		get { return throttle; }
	}

	public void SetThrottle(float normalizedThrottle)
	{
		throttle = normalizedThrottle;

		// Play any attached sound effect:
		if (sfx != null)
		{
			if (throttle > 0)
			{
				sfx.volume = throttle;
				if (!sfx.isPlaying)
					sfx.Play();
			}
			else if (sfx.isPlaying)
			{
				sfx.Stop();
			}
		}
	}

	void Update()
	{
		particles.emissionRate = throttle * maxExhaust;
		particles.enableEmission = true;

		if (engineLight != null)
		{
			if (throttle == 0)
			{
				engineLight.intensity = Mathf.Max(0, engineLight.intensity - engineLightIntensity * (Time.deltaTime / engineFadeTime));
			}
			else
				engineLight.intensity = engineLightIntensity - (engineFlickerAmount * Mathf.PerlinNoise(0, Time.time * engineFlickerRate));
		}
	}

	// FixedUpdate is called once per physics clock tick (50fps by default)
	void FixedUpdate()
	{
		// Apply force pointing directly up relative to the engine's orientation
		affectedBody.AddForce(maxThrust * throttle * transform.up);
	}
}
