using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public enum PlayingState
	{
		Running,
		Smarmotting,
		Dialoguing,
	}

	public static GameManager Instance;

	public PlayingState CurrentState = PlayingState.Running;

	private void Awake()
	{
		if (!Instance)
		{
			Instance = this;
		}
		else if (Instance != this)
			Destroy(gameObject);

		DontDestroyOnLoad(this);
	}

	// Start is called before the first frame update
	private void Start()
	{

	}

	// Update is called once per frame
	private void Update()
	{

	}
}
