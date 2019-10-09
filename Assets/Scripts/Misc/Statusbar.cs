using UnityEngine;
using System.Collections;

// Draws a status bar above/below an object
public class Statusbar : MonoBehaviour 
{
	public string barTextureName = "statusbar";	// The name of our bar texture (must be located in Resources folder)
	public Gradient colorGrad;
	public Vector2 offset;
	public Vector2 drawSize = new Vector2(5f, 1f);

	protected Texture2D barTexture;
	protected float targetPercent = 1f;		// The normalized percentage the bar is to represent
	protected float displayPercent = 1f;	// The value actually used to render the bar, allows us to animate the bar toward the targetPercent value
	protected Rect srcRect;					// The rect of the area to use from the source texture
	protected Rect scrExtents;				// The rect that defines the screen area extents

	protected Vector2 size;


	void Awake()
	{
		barTexture = Resources.Load(barTextureName, typeof(Texture2D)) as Texture2D;
		srcRect = new Rect(0,0,1f,1f);
	}

	void Start()
	{
		scrExtents = Camera.main.pixelRect;
		size = drawSize / GetWorldUnitsPerPixel(Camera.main);
	}

	public float Percent
	{
		get { return targetPercent; }
		set { targetPercent = Mathf.Clamp01(value); }
	}

	void Update()
	{
		displayPercent += (targetPercent - displayPercent) * Time.deltaTime * 6f;
	}

	void OnGUI()
	{
		if (!Event.current.type.Equals(EventType.Repaint) || LevelStatusLogic.LevelStatus != LevelStatusLogic.LevelStatusCode.Playing)
			return;
#if UNITY_EDITOR
		if (!Application.isPlaying)
			size = drawSize / GetWorldUnitsPerPixel(Camera.main);
#endif

		Vector2 pos = Camera.main.WorldToScreenPoint(transform.position + Camera.main.transform.TransformDirection(offset));
		pos.y = Camera.main.pixelHeight - pos.y;

		// Don't render the bar if the object is out of the screen bounds:
		if (!scrExtents.Contains(pos))
			return;

		Rect screenRect = new Rect(pos.x - size.x * 0.5f, pos.y - size.y * 0.5f,
		                           size.x * displayPercent, size.y);
		srcRect.width = displayPercent;

		Graphics.DrawTexture(screenRect, barTexture, srcRect, 0, 0, 0, 0, colorGrad.Evaluate(displayPercent));
	}

	public float GetWorldUnitsPerPixel(Camera cam)
	{
		if (cam == null)
			return 1f;

		Vector3 screenPos = cam.WorldToScreenPoint(transform.position);
		screenPos.x += 1f;
		Vector3 p2 = cam.ScreenToWorldPoint(screenPos);

		return Vector3.Distance(transform.position, p2);
	}
}
