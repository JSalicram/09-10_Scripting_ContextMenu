using UnityEngine;
using System.Collections;

public class LevelStatusLogic : MonoBehaviour 
{
	public enum LevelStatusCode
	{
		Playing,
		Won,
		Lost
	};

	public TextMesh hudText;

	protected static LevelStatusCode levelStatus;
	protected static LevelStatusLogic instance;


	void Awake()
	{
		instance = this;
	}

	public static LevelStatusLogic Instance
	{
		get { return instance; }
	}

	public static LevelStatusCode LevelStatus
	{
		get { return levelStatus; }
	}

	public static void PlayerDied()
	{
		levelStatus = LevelStatusCode.Lost;
		Time.timeScale = 0.1f;
	}

	protected virtual void Update()
	{
		switch(levelStatus)
		{
		case LevelStatusCode.Playing:
			break;
		case LevelStatusCode.Won:
			hudText.text = "You landed!";
			break;
		case LevelStatusCode.Lost:
			break;
		}
	}

	public void PlayerHasLanded()
	{
		levelStatus = LevelStatusCode.Won;
	}
}
