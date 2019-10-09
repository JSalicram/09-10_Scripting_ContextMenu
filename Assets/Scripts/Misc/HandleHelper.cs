using UnityEngine;
using System.Collections;

public static class HandleHelper
{
	public static Vector3 GetVectorFromAngle(float degrees)
	{
		float rads = degrees * Mathf.Deg2Rad;
		return new Vector3(Mathf.Cos(rads), Mathf.Sin(rads), 0);
	}
	
	public static float GetAngleFromHandlePos(Vector3 handlePos, Transform trans = null)
	{
		return GetAngleFromHandlePos(handlePos, (trans == null) ? Vector3.zero : trans.position, trans);
	}

	public static float GetAngleFromHandlePos(Vector3 handlePos, Vector3 refPoint, Transform trans = null)
	{
		Vector3 heading = Vector3.zero;

		// If we have a transform, transform the vector between the 
		// reference point and the handle's position.
		if (trans != null)
			heading = trans.InverseTransformDirection(handlePos - refPoint);
		else
			heading = handlePos - refPoint;

		heading.Normalize();
		return Mathf.Repeat(Mathf.Atan2(heading.y, heading.x) * Mathf.Rad2Deg, 360f);
	}
}
