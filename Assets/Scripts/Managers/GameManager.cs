using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public enum PlayingState
	{
		Running,
		Smarmotting,
		Dialoguing,
	}

	public static GameManager Instance;

	public PlayingState CurrentState = PlayingState.Running;

	private void Awake()
	{
		if (!Instance)
		{
			Instance = this;
		}
		else if (Instance != this)
			Destroy(gameObject);

		DontDestroyOnLoad(this);
	}

	public void StateChanged(PlayingState state)
	{
		CurrentState = state;
	}

	public void QuicktimeEnded(bool result)
	{
		//aggiungere controllo
		EventManager.Instance.OnPlayingStateChanged.Invoke(PlayingState.Running);
	}

	// Start is called before the first frame update
	private void Start()
	{
		EventManager.Instance.OnPlayingStateChanged.AddListener(StateChanged);
		EventManager.Instance.OnQuickTimeSuccess.AddListener(QuicktimeEnded);
	}

	// Update is called once per frame
	private void Update()
	{

	}
}
