using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
	public static InputManager Instance;

	public enum ControllerButtons { A, B, X, Y };

	private void Awake()
	{
		if (!Instance)
		{
			Instance = this;
		}
		else if (Instance != this)
			Destroy(gameObject);
	}

	// Start is called before the first frame update
	private void Start()
	{

	}

	//CONTROLLO BRUTTO TEMPORANEO
	private void Update()
	{
		if (GameManager.Instance.CurrentState != GameManager.PlayingState.Running)
		{
			if (Input.GetButtonDown("A"))
			{
				EventManager.Instance.OnButtonPressed.Invoke(ControllerButtons.A);
			}
			if (Input.GetButtonDown("B"))
			{
				EventManager.Instance.OnButtonPressed.Invoke(ControllerButtons.B);
			}
			if (Input.GetButtonDown("X"))
			{
				EventManager.Instance.OnButtonPressed.Invoke(ControllerButtons.X);
			}
			if (Input.GetButtonDown("Y"))
			{
				EventManager.Instance.OnButtonPressed.Invoke(ControllerButtons.Y);
			}
		}
	}
}
