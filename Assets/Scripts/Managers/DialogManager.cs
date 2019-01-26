﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
	[Serializable]
	public class Dialogs
	{
		[Serializable]
		public class Dialog
		{
			public int id;
			public DialogTexts dialog;
		}

		[Serializable]
		public class DialogTexts
		{
			public string text;
			public Answer[] answers;
		}

		[Serializable]
		public class Answer
		{
			public string text;
			public int idtojumpto;
		}

		public Dialog[] dialogs;
	}

	public string NPC;
	public Dialogs _dialogs;

	[SerializeField] private Image _dialogPanel;
	[SerializeField] private TextMeshProUGUI _dialogText;
	[SerializeField] private TextMeshProUGUI[] _answers;

	private int _dialogStepIndex = 0;

	private void Start()
	{
		_dialogs = ReadAndParseJson<Dialogs>(Path.Combine(Application.streamingAssetsPath, NPC + ".json"));

		_dialogPanel.gameObject.SetActive(false);

		EventManager.Instance.OnPlayingStateChanged.AddListener(StartDialog);
	}

	private T ReadAndParseJson<T>(string path)
	{
		var jsonText = File.ReadAllText(path);
		return JsonUtility.FromJson<T>(jsonText);
	}

	private void StartDialog(GameManager.PlayingState state)
	{
		if (state == GameManager.PlayingState.Dialoguing)
		{
			_dialogPanel.gameObject.SetActive(true);
			NextDialogStep();
		}
		else if (state == GameManager.PlayingState.Smarmotting)
		{
			QuitDialog();
		}
	}

	public void NextDialogStep()
	{
		if (_dialogStepIndex != -1)
		{
			_dialogText.text = _dialogs.dialogs[_dialogStepIndex].dialog.text;
			var answers = _dialogs.dialogs[_dialogStepIndex].dialog.answers;
			for (int i = 0; i < _answers.Length; i++)
			{
				if (i < answers.Length)
					_answers[i].text = answers[i].text;
				else
					_answers[i].text = "";
			}

			EventManager.Instance.OnButtonPressed.AddListener(CheckAnswer);
		}
		else
		{
			EventManager.Instance.OnButtonPressed.AddListener(EndDialog);
		}
	}

	private void EndDialog(InputManager.ControllerButtons arg)
	{
		_dialogPanel.gameObject.SetActive(false);

		EventManager.Instance.OnButtonPressed.RemoveListener(EndDialog);
		EventManager.Instance.OnPlayingStateChanged.Invoke(GameManager.PlayingState.Running);
	}

	private void QuitDialog()
	{
		_dialogPanel.gameObject.SetActive(false);
	}

	private void CheckAnswer(InputManager.ControllerButtons buttonPressed)
	{
		var answers = _dialogs.dialogs[_dialogStepIndex].dialog.answers;
		switch (buttonPressed)
		{
			case InputManager.ControllerButtons.A:
				_dialogStepIndex = answers[0].idtojumpto;
				break;
			case InputManager.ControllerButtons.B:
				if (answers.Length > 1)
					_dialogStepIndex = answers[1].idtojumpto;
				break;
			case InputManager.ControllerButtons.X:
				if (answers.Length > 2)
					_dialogStepIndex = answers[2].idtojumpto;
				break;
			case InputManager.ControllerButtons.Y:
				if (answers.Length > 3)
					_dialogStepIndex = answers[3].idtojumpto;
				break;
		}

		EventManager.Instance.OnButtonPressed.RemoveListener(CheckAnswer);
		NextDialogStep();
	}
}
