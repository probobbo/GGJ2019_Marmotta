using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DialogManager : MonoBehaviour
{
	[Serializable]
	public class Dialogs
	{
		public Dialog[] dialogs;
	}

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

	public Dialogs _dialogs;

	// Start is called before the first frame update
	private void Start()
	{
		_dialogs = ReadAndParseJson<Dialogs>(Path.Combine(Application.streamingAssetsPath, "vecchia.json"));
	}

	private T ReadAndParseJson<T>(string path)
	{
		var jsonText = File.ReadAllText(path);
		return JsonUtility.FromJson<T>(jsonText);
	}

	// Update is called once per frame
	private void Update()
	{

	}
}
