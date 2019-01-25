using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickTimeBar : MonoBehaviour
{

	private List<QuickTimeButton> _buttons;
	public GameObject ButtonPrefab;
	public int MaxNumberOfButtons = 5;

	private void OnEnable()
	{
		_buttons = new List<QuickTimeButton>();
	}

	private void Start()
	{
		EventManager.Instance.OnQuickTimeButtonPressed.AddListener(CheckButton);
	}

	public void AddButton()
	{
		if (_buttons.Count == MaxNumberOfButtons)
			PopFirst();
		Swipe();
		GameObject g = Instantiate(ButtonPrefab, transform);
		_buttons.Add(g.GetComponent<QuickTimeButton>());
	}

	//todo
	public void CheckButton(InputManager.ControllerButtons button)
	{
		if (_buttons.Count > 0 && _buttons[0].GetButtonType() == button)
		{
			PopFirst();
			Swipe();
		}
	}

	private void PopFirst()
	{
		Destroy(_buttons[0].gameObject);
		_buttons.RemoveAt(0);
		if (_buttons.Count == 0)
		{
			EventManager.Instance.OnQuickTimeSuccess.Invoke(true);
		}
	}

	private void Swipe()
	{
		foreach (var b in _buttons)
		{
			b.SwipeLeft();
		}
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.A))
		{
			AddButton();
		}
	}
	//
}
