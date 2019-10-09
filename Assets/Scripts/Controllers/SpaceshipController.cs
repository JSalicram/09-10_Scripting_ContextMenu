using UnityEngine;
using System.Collections;

// Handles user input to control a spaceship object
public class SpaceshipController : MonoBehaviour 
{
	// Identifiers for various input types:
	public enum Input_Type
	{
		Keyboard			// Keyboard input
	};

	delegate void InputPollingDelegate();


	public Spaceship shipToControl;
	public Input_Type inputType = Input_Type.Keyboard;

	InputPollingDelegate inputPoller;

	void Awake () 
	{
		// If one wasn't assigned in the inspector,
		// get a reference to any Spaceship attached
		// to the same GameObject as this component:
		if (shipToControl == null)
			shipToControl = GetComponent<Spaceship>();

		// If ship is still null, log an error:
		if (shipToControl == null)
			Debug.LogError("No spaceship found for controller!");

		// Assign the appropriate delegate to poll for
		// input based on our selected input type:
		switch(inputType)
		{
		case Input_Type.Keyboard:
			inputPoller = PollKeyboard;
			break;
		}
	}

	// Keyboard polling delegate
	void PollKeyboard()
	{
		// Right arrow (turn clockwise):
		if (Input.GetKey(KeyCode.RightArrow))
			shipToControl.Rotate(-1f);

		// Left arrow (turn counter-clockwise):
		if (Input.GetKey(KeyCode.LeftArrow))
			shipToControl.Rotate(1f);

		// Up arrow (thrust):
		if (Input.GetKey(KeyCode.UpArrow))
			shipToControl.Throttle = 1f;
		else
			shipToControl.Throttle = 0;
	}
	
	// Update is called once per frame
	void Update () 
	{
		// Poll input:
		inputPoller();
	}
}
