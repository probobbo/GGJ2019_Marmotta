using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using Managers;
using System;
using DG.Tweening;

public class PostProcessingTweener : MonoBehaviour
{
	[SerializeField] private PostProcessVolume _farPPV;
	[SerializeField] private PostProcessVolume _nearPPV;
	private GameManager.PlayingState _currentState;

	private void Start()
	{
		_currentState = GameManager.Instance.CurrentState;

		EventManager.Instance.OnPlayingStateChanged.AddListener(ChangePPVWeight);
	}

	private void ChangePPVWeight(GameManager.PlayingState state)
	{
		if (state == GameManager.PlayingState.Running)
		{
			DOTween.To(() => _farPPV.weight, value => _farPPV.weight = value, 1f, 0.5f);
			DOTween.To(() => _nearPPV.weight, value => _nearPPV.weight = value, 0f, 0.5f);
		}
		else if (state == GameManager.PlayingState.Dialoguing || state == GameManager.PlayingState.Smarmotting)
		{
			DOTween.To(() => _farPPV.weight, value => _farPPV.weight = value, 0f, 0.5f);
			DOTween.To(() => _nearPPV.weight, value => _nearPPV.weight = value, 1f, 0.5f);
		}
	}
}
