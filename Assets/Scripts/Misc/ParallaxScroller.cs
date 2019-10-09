using UnityEngine;
using System.Collections;

public class ParallaxScroller : MonoBehaviour 
{
	public Vector2 rate;						// The rate of the parallax motion relative to the source's motion on each axis
	public Transform motionSource;				// The source of the motion to which we wish to assciate the parallax movement

	Material material;

	void Awake()
	{
		material = GetComponent<Renderer>().material;
	}

	// Update is called once per frame
	void Update () 
	{
		material.mainTextureOffset = Vector2.Scale(rate, motionSource.position);
	}
}
