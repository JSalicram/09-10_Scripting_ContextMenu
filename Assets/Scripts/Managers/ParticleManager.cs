using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// Manages all bullets in the game so we can recycle
// bullets rather than instantiating and destroying.
public class ParticleManager : MonoBehaviour
{
	public ParticleSystem sparks;
	public ParticleSystem explosion;

	private static ParticleManager instance;


	void Awake()
	{
		instance = this;
	}

	public static ParticleManager Instance
	{
		get { return instance; }
	}

	public static void EmitSparks(int count, Vector2 pos)
	{
		instance.sparks.transform.position = pos;
		instance.sparks.Emit(count);
	}

	public static void EmitExplosion(int count, Vector2 pos)
	{
		instance.explosion.transform.position = pos;
		instance.explosion.Emit(count);
	}
}