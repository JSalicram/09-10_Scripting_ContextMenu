using UnityEngine;
using System.Collections;

public class FollowCamera : MonoBehaviour 
{
	public Transform target;				// The target the camera should follow

	Vector3 offset;							// The offset of the camera to the target, setup at edit time and this will save it at runtime


	void Awake()
	{
		offset = transform.position - target.position;
	}

	void LateUpdate () 
	{
		if (target == null)
			return;
		else if (!target.gameObject.activeInHierarchy)
			return;

		// Position the camera:
		transform.position = target.position + offset;

		transform.LookAt(target);
	}
}
