using DG.Tweening;
using Managers;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NPCAnswer : MonoBehaviour
{
	[SerializeField] private InputManager.ControllerButtons _answerButton;
	[SerializeField] private Color _selectedColor = Color.green;

	private Image _buttonPanel;
	private TextMeshProUGUI _text;
	private bool _isDialoguing = false;

	private void Start()
	{
		_buttonPanel = GetComponent<Image>();
		_text = GetComponentInChildren<TextMeshProUGUI>();

		EventManager.Instance.OnPlayingStateChanged.AddListener(UpdateUI);
	}

	private void UpdateUI(GameManager.PlayingState state)
	{
		if (state == GameManager.PlayingState.Dialoguing)
		{
			_isDialoguing = true;
			EventManager.Instance.OnButtonPressed.AddListener(BlinkButton);
		}
		else if (_isDialoguing)
		{
			EventManager.Instance.OnButtonPressed.RemoveListener(BlinkButton);
			_isDialoguing = false;
		}
	}

	private void BlinkButton(InputManager.ControllerButtons button)
	{
		if (button == _answerButton && !String.IsNullOrEmpty(_text.text))
			_buttonPanel.DOColor(_selectedColor, 0.05f).SetLoops(2, LoopType.Yoyo);
	}
}
