using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickTimeBar : MonoBehaviour
{

	private List<QuickTimeButton> _buttons;
	public GameObject ButtonPrefab;

	private void Start()
	{
		_buttons = new List<QuickTimeButton>();
	}

	public void AddButton()
	{
		GameObject g = Instantiate(ButtonPrefab, transform);
		_buttons.Add(g.GetComponent<QuickTimeButton>());
	}

	//todo
	public void CheckButton() { }

	//
}
