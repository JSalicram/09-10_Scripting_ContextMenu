using UnityEngine;
using System.Collections;


public class ScreenPlacer : MonoBehaviour 
{
	public Vector3 screenPos;

	// Use this for initialization
	void Start () 
	{
		Vector3 pos = screenPos;
		pos.y = Camera.main.pixelHeight - pos.y;
		transform.position = Camera.main.ScreenToWorldPoint(pos);
	}
}
