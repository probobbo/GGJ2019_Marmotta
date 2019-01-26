using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Managers;

public class CameraController : MonoBehaviour
{
	[SerializeField] private Transform _dialogCameraOffset;
	[SerializeField] private float _cameraTweenDuration = 0.5f;

    private Transform _player;
	private Transform _camera;
	private GameManager.PlayingState _currentState;

	private Vector3 _runningCameraPositionOffset;
	private Quaternion _runningCameraRotationOffset;

    private void Start()
    {
		_camera = Camera.main.transform;
        _player = GameObject.FindGameObjectWithTag("Player").transform;
		_currentState = GameManager.Instance.CurrentState;

		EventManager.Instance.OnPlayingStateChanged.AddListener(ChangeCameraOffset);
    }

	private void ChangeCameraOffset(GameManager.PlayingState state)
	{
		if (_currentState == GameManager.PlayingState.Running)
		{
			_runningCameraPositionOffset = _camera.position;
			_runningCameraRotationOffset = _camera.rotation;
		}

		if (state == GameManager.PlayingState.Dialoguing || state == GameManager.PlayingState.Smarmotting)
		{
			_camera.DOMove(_dialogCameraOffset.position, _cameraTweenDuration);
			_camera.DORotate(_dialogCameraOffset.rotation.eulerAngles, _cameraTweenDuration);
		}
		else if (state == GameManager.PlayingState.Running)
		{
			_camera.DOMove(_runningCameraPositionOffset, _cameraTweenDuration);
			_camera.DORotate(_runningCameraRotationOffset.eulerAngles, _cameraTweenDuration);
		}

		_currentState = state;
	}

	private void Update()
    {
        transform.position = _player.position;
    }
}
