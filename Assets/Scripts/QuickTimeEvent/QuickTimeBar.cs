using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickTimeBar : MonoBehaviour
{

	private List<QuickTimeButton> _buttons;
	private Coroutine _timeLimitCoroutine;
	public GameObject ButtonPrefab;
	public int MaxNumberOfButtons = 5;
	public float TimeLimit = 5.0f;

	private void Start()
	{
		_buttons = new List<QuickTimeButton>();
		EventManager.Instance.OnButtonPressed.AddListener(CheckButton);
		EventManager.Instance.OnQuickTimeEventStart.AddListener(StartQuickTimeEvent);
	}

	public void StartQuickTimeEvent(float difficulty)
	{
		EventManager.Instance.OnPlayingStateChanged.Invoke(GameManager.PlayingState.Smarmotting);
		_buttons = new List<QuickTimeButton>();
		_timeLimitCoroutine = StartCoroutine(ButtonSpawner(difficulty));
	}

	public void Clear()
	{
		foreach(var b in _buttons)
		{
			Destroy(b.gameObject);
		}
		_buttons.Clear();
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
		}
	}

	private void PopFirst()
	{
		Destroy(_buttons[0].gameObject);
		_buttons.RemoveAt(0);
		if (_buttons.Count == 0)
		{
			if (_timeLimitCoroutine != null)
				StopCoroutine(_timeLimitCoroutine);
			Clear();
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

	IEnumerator ButtonSpawner(float difficulty)
	{
		float timer = 0.0f;
		while (timer < TimeLimit)
		{
			timer += difficulty;
			Debug.Log(timer);
			AddButton();
			yield return new WaitForSeconds(difficulty);
		}
		Clear();
		EventManager.Instance.OnQuickTimeSuccess.Invoke(false);
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.A))
		{
			EventManager.Instance.OnQuickTimeEventStart.Invoke(.2f);
		}
	}
	//
}
