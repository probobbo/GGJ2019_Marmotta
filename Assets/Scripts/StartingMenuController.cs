using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartingMenuController : MonoBehaviour
{
	[SerializeField]
	private Button _start;

	private void Start()
	{
		_start.onClick.AddListener(()=> { SceneManager.LoadScene(1); });
	}
}
