using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Managers;

public class PlayerController : MonoBehaviour
{
	public float MovementSpeed = 5.0f;
	[SerializeField] private float _lookAtNpcTweenDuration = 0.5f;
	private float _e = .05f;

	public void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("NPC"))
		{/*
			var npcTransform = other.transform;
			transform.DOLookAt(npcTransform.position, _lookAtNpcTweenDuration);*/
		}
		else if (other.CompareTag("Finish"))
		{
			EventManager.Instance.OnPlayingStateChanged.Invoke(GameManager.PlayingState.Won);
		}
	}

	private void Update()
	{
		if (GameManager.Instance.CurrentState == GameManager.PlayingState.Running)
		{
			float forwardWeight = Input.GetAxis("Vertical");
			float turningWeight = Input.GetAxis("Horizontal");
			var direction = new Vector3(turningWeight, 0f, forwardWeight);
			transform.Translate(direction * MovementSpeed * Time.deltaTime, Space.World);
			/*if (Mathf.Abs(forwardWeight) >= _e || Mathf.Abs(turningWeight) >= _e)
				transform.forward = direction;*/
		}
		else
		{
			if (Input.GetButtonDown("A") || Input.GetKeyDown(KeyCode.K))
			{
				EventManager.Instance.OnButtonPressed.Invoke(InputManager.ControllerButtons.A);
			}
			if (Input.GetButtonDown("B") || Input.GetKeyDown(KeyCode.L))
			{
				EventManager.Instance.OnButtonPressed.Invoke(InputManager.ControllerButtons.B);
			}
			if (Input.GetButtonDown("X") || Input.GetKeyDown(KeyCode.J))
			{
				EventManager.Instance.OnButtonPressed.Invoke(InputManager.ControllerButtons.X);
			}
			if (Input.GetButtonDown("Y") || Input.GetKeyDown(KeyCode.I))
			{
				EventManager.Instance.OnButtonPressed.Invoke(InputManager.ControllerButtons.Y);
			}
		}
	}
}
