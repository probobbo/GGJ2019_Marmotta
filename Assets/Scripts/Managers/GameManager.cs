﻿using System.Collections;
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

	private float _smarmotTimer = 0.0f;
	private float _currentSmarmotTimeLimit;
	private float _currentQuickTimeFrequency;

	public float StartingSmarmotTimer = 10.0f;
	public float SmarmotTimerDeltadecrease = 2.0f;
	public float QuickTimeStartingFrequency = 1.5f;
	public float QuickTimeDeltaDecrease = 0.2f;

	private void Awake()
	{
		if (!Instance)
		{
			Instance = this;
		}
		else if (Instance != this)
			Destroy(gameObject);
		
	}

	public void StateChanged(PlayingState state)
	{
		CurrentState = state;
	}

	public void QuicktimeEnded(bool result)
	{
		//aggiungere controllo
		EventManager.Instance.OnPlayingStateChanged.Invoke(PlayingState.Running);
	}

	// Start is called before the first frame update
	private void Start()
	{
		EventManager.Instance.OnPlayingStateChanged.AddListener(StateChanged);
		EventManager.Instance.OnQuickTimeSuccess.AddListener(QuicktimeEnded);
		_smarmotTimer = 0;
	}

	// Update is called once per frame
	private void Update()
	{
		_smarmotTimer += Time.deltaTime;
		if (_smarmotTimer >= StartingSmarmotTimer)
		{
			EventManager.Instance.OnQuickTimeEventStart.Invoke(_currentQuickTimeFrequency);
		}
	}

	private void OnDestroy()
	{
		EventManager.Instance.OnPlayingStateChanged.RemoveListener(StateChanged);
		EventManager.Instance.OnQuickTimeSuccess.RemoveListener(QuicktimeEnded);
	}
}
