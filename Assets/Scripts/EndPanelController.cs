using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndPanelController : MonoBehaviour
{
	[SerializeField]
	private Button _retry;
	[SerializeField]
	private Button _back;
	[SerializeField]
	private GameObject _endPanel;
	// Start is called before the first frame update
	void Start()
	{
		_retry.onClick.AddListener(() =>
		{
			SceneManager.LoadScene(1);
		});

		_back.onClick.AddListener(()=>
		{
			SceneManager.LoadScene(0);
		});
		_endPanel.SetActive(false);
		EventManager.Instance.OnPlayingStateChanged.AddListener(PopUp);
	}

	private void PopUp(GameManager.PlayingState state)
	{
		if (state == GameManager.PlayingState.End)
		{
			_endPanel.SetActive(true);
		}
	}
}
