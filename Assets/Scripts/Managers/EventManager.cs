﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventManager : MonoBehaviour
{
	public class StateChangedEvent : UnityEvent<GameManager.PlayingState> { }
	public class ButtonPressed : UnityEvent<InputManager.ControllerButtons> { }
	public class QuickTimeSuccess : UnityEvent<bool> { }
	public class QuickTimeEventStart : UnityEvent<float> { }

	public static EventManager Instance;

	public UnityEvent OnGameStarted;
	public QuickTimeSuccess OnQuickTimeSuccess;
	public StateChangedEvent OnPlayingStateChanged;
	public ButtonPressed OnButtonPressed;
	public QuickTimeEventStart OnQuickTimeEventStart;

	private void Awake()
	{
		if (!Instance)
		{
			Instance = this;

			OnGameStarted = new UnityEvent();
			OnQuickTimeSuccess = new QuickTimeSuccess();
			OnPlayingStateChanged = new StateChangedEvent();
			OnButtonPressed = new ButtonPressed();
			OnQuickTimeEventStart = new QuickTimeEventStart();
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
