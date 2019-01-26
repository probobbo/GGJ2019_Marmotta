using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Managers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
	private enum DialogState
	{
		None,
		Dialoguing,
		Answering,
		Ending,
	}

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
	private DialogState _dialogState = DialogState.None;

	[SerializeField] private Image _dialogPanel;
	[SerializeField] private TextMeshProUGUI _dialogText;
	[SerializeField] private TextMeshProUGUI[] _answers;

	private int _dialogStepIndex = 0;
	private bool _dialogStarted = false;

	private void Start()
	{
		_dialogs = ReadAndParseJson<Dialogs>(Path.Combine(Application.streamingAssetsPath, NPC + ".json"));

		_dialogPanel.gameObject.SetActive(false);

		EventManager.Instance.OnPlayingStateChanged.AddListener(QuitDialog);
	}

	private T ReadAndParseJson<T>(string path)
	{
		var jsonText = File.ReadAllText(path);
		return JsonUtility.FromJson<T>(jsonText);
	}

	public void StartDialog()
	{
		_dialogState = DialogState.Dialoguing;
		StartCoroutine(LookAtCamera(Camera.main.transform));
		_dialogPanel.gameObject.SetActive(true);
		NextDialogStep();
	}

	private IEnumerator LookAtCamera(Transform cameraTransform)
	{
		float t = 0f;
		while (t < 0.5f)
		{
			yield return new WaitForEndOfFrame();
			transform.LookAt(cameraTransform.position);
			transform.Rotate(new Vector3(0f, 180f, 0f));
			t += Time.deltaTime;
		}
	}

	public void NextDialogStep()
	{
		_dialogText.text = _dialogs.dialogs[_dialogStepIndex].dialog.text;
		var answers = _dialogs.dialogs[_dialogStepIndex].dialog.answers;
		for (int i = 0; i < _answers.Length; i++)
		{
			if (i < answers.Length)
				_answers[i].text = ((InputManager.ControllerButtons)i).ToString() + " " + answers[i].text;
			else
				_answers[i].text = "";
		}

		EventManager.Instance.OnButtonPressed.AddListener(CheckAnswer);
	}

	private void EndDialog()
	{
		_dialogState = DialogState.Ending;
		_dialogPanel.gameObject.SetActive(false);
		EventManager.Instance.OnPlayingStateChanged.Invoke(GameManager.PlayingState.Running);
	}

	private void QuitDialog(GameManager.PlayingState playingState)
	{
		if (playingState == GameManager.PlayingState.Smarmotting)
		{
			switch (_dialogState)
			{
				case DialogState.Answering:
					EventManager.Instance.OnButtonPressed.RemoveListener(CheckAnswer);
					break;
				default:
					break;
			}

			_dialogPanel.gameObject.SetActive(false);
		}
	}

	private void CheckAnswer(InputManager.ControllerButtons buttonPressed)
	{
		_dialogState = DialogState.Answering;
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

		if (_dialogStepIndex != -1)
			NextDialogStep();
		else
			EndDialog();
	}
}
