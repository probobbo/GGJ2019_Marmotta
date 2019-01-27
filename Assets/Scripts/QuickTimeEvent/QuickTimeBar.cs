using System.Collections;
using System.Collections.Generic;
using Managers;
using UnityEngine;
using UnityEngine.UI;

public class QuickTimeBar : MonoBehaviour
{

	private List<QuickTimeButton> _buttons;
	private Slider _slider;
	private Coroutine _timeLimitCoroutine;
	public GameObject ButtonPrefab;
	public int MaxNumberOfButtons = 5;
	public int StartingNumberOfButtons = 3;
	public float TimeLimit = 5.0f;
	private float _timer = 0;

	[SerializeField] private float _timeMalus = 0.5f;

	private void Start()
	{
		_buttons = new List<QuickTimeButton>();
		EventManager.Instance.OnButtonPressed.AddListener(CheckButton);
		EventManager.Instance.OnQuickTimeEventStart.AddListener(StartQuickTimeEvent);
		_slider = GetComponentInChildren<Slider>();
		_slider.maxValue = 100;
		_slider.value = _slider.maxValue;
		_slider.gameObject.SetActive(false);
	}

	public void StartQuickTimeEvent(float difficulty)
	{
		EventManager.Instance.OnPlayingStateChanged.Invoke(GameManager.PlayingState.Smarmotting);
		_buttons = new List<QuickTimeButton>();
		_timeLimitCoroutine = StartCoroutine(ButtonSpawner(difficulty));
	}

	public void Clear()
	{
		_slider.gameObject.SetActive(false);
		foreach (var b in _buttons)
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
		else
		{
			_timer += _timeMalus;
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
		_timer = 0.0f;
		float t = 0;
		for (int i = 0; i < StartingNumberOfButtons; i++)
		{
			AddButton();
			yield return null;
		}
		_slider.gameObject.SetActive(true);
		_slider.value = _slider.maxValue;
		while (_timer < TimeLimit)
		{
			_timer += Time.deltaTime;
			_slider.value = 100 -((_timer * 100) / TimeLimit);
			t += Time.deltaTime;
			if (t >= difficulty)
			{
				t = 0;
				AddButton();
			}
			yield return new WaitForEndOfFrame();
		}
		Clear();
		EventManager.Instance.OnQuickTimeSuccess.Invoke(false);
	}
}
