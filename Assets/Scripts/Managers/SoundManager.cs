using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// Manages all bullets in the game so we can recycle
// bullets rather than instantiating and destroying.
public class SoundManager : MonoBehaviour
{
	public float masterVolume = 1f;
	public AudioSource explosion;
	public AudioSource miscImpact;

	private static SoundManager instance;


	void Awake()
	{
		instance = this;
	}

	void Start()
	{
		AudioListener.volume = masterVolume;
	}

	public static SoundManager Instance
	{
		get { return instance; }
	}

	public static void PlayExplosion(Vector2 pos)
	{
		instance.explosion.transform.position = pos;
		instance.explosion.PlayOneShot(instance.explosion.clip);
	}

	public static void PlayImpact(Vector2 pos)
	{
		instance.miscImpact.transform.position = pos;
		instance.miscImpact.PlayOneShot(instance.miscImpact.clip);
	}
}
