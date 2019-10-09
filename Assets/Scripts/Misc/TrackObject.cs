using UnityEngine;
using System.Collections;

// Causes the GameObject to which the TrackObject component
// is attached to follow the target object in 2D
public class TrackObject : MonoBehaviour 
{
	public Transform target;

	protected Vector3 offset;				// Saved offset from the object to be tracked.  Based on the relative position at Start().


	void Start()
	{
		offset = transform.position - target.position;
	}

	// Update is called once per frame
	void Update () 
	{
		transform.position = target.position + offset;
	}

}
