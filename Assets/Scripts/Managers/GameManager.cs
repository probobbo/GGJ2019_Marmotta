﻿using System.Collections;
using System.Collections.Generic;
using Managers;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public enum PlayingState
	{
		Running,
		Smarmotting,
		Dialoguing,
		Lost,
		Won,
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

	public bool CanChangeState;


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
		if (CanChangeState)
		{
			if (state == PlayingState.Smarmotting)
			{
				CanChangeState = false;
			}
			CurrentState = state;
		}
	}

	public void QuicktimeEnded(bool result)
	{
		CanChangeState = true;
		if (result)
		{
			EventManager.Instance.OnPlayingStateChanged.Invoke(PlayingState.Running);
		}
		else
		{
			EventManager.Instance.OnPlayingStateChanged.Invoke(PlayingState.Lost);
		}
	}

	// Start is called before the first frame update
	private void Start()
	{
		EventManager.Instance.OnPlayingStateChanged.AddListener(StateChanged);
		EventManager.Instance.OnQuickTimeSuccess.AddListener(QuicktimeEnded);
		SetUp();
	}

	private void SetUp()
	{
		CanChangeState = true;
		_smarmotTimer = 0;
		_currentQuickTimeFrequency = QuickTimeStartingFrequency;
		_currentSmarmotTimeLimit = StartingSmarmotTimer;
		CurrentState = PlayingState.Running;
	}
	// Update is called once per frame
	private void Update()
	{
		if (CurrentState == PlayingState.Running || CurrentState == PlayingState.Dialoguing)
		{
			_smarmotTimer += Time.deltaTime;
			if (_smarmotTimer >= _currentSmarmotTimeLimit)
			{
				EventManager.Instance.OnQuickTimeEventStart.Invoke(_currentQuickTimeFrequency);
				_smarmotTimer = 0;
				_currentQuickTimeFrequency -= QuickTimeDeltaDecrease;
				_currentSmarmotTimeLimit -= SmarmotTimerDeltadecrease;
			}
		}
	}

	public float GetSmarmotBarValue()
	{
		return (_smarmotTimer * 100) / _currentSmarmotTimeLimit;
	}

	private void OnDestroy()
	{
		EventManager.Instance.OnPlayingStateChanged.RemoveListener(StateChanged);
		EventManager.Instance.OnQuickTimeSuccess.RemoveListener(QuicktimeEnded);
	}
}
