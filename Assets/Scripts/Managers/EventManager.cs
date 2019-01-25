using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventManager : MonoBehaviour
{
	public class StateChangedEvent : UnityEvent<GameManager.PlayingState> { }

	public static EventManager Instance;

	public UnityEvent OnGameStarted;
	public StateChangedEvent OnPlayingStateChanged;

	private void Awake()
	{
		if (!Instance)
		{
			Instance = this;

			OnGameStarted = new UnityEvent();
			OnPlayingStateChanged = new StateChangedEvent();
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
