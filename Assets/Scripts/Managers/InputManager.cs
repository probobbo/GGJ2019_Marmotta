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
		if (Input.GetButtonDown("Fire1"))
		{
			EventManager.Instance.OnQuickTimeButtonPressed.Invoke(ControllerButtons.A);
		}
		if (Input.GetButtonDown("Fire2"))
		{
			EventManager.Instance.OnQuickTimeButtonPressed.Invoke(ControllerButtons.B);
		}
		if (Input.GetButtonDown("Fire3"))
		{
			EventManager.Instance.OnQuickTimeButtonPressed.Invoke(ControllerButtons.X);
		}
		if (Input.GetButtonDown("Jump"))
		{
			EventManager.Instance.OnQuickTimeButtonPressed.Invoke(ControllerButtons.Y);
		}
	}
}
