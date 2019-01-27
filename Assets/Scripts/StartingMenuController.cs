using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartingMenuController : MonoBehaviour
{
	[SerializeField] private Button _start;
	[SerializeField] private Button _credits;


	private void Start()
	{
		_start.onClick.AddListener(()=> { SceneManager.LoadScene(1); });
		_credits.onClick.AddListener(()=> { SceneManager.LoadScene(2); });

	}
}
