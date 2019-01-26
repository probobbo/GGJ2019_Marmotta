using System.Collections;
using System.Collections.Generic;
using Managers;
using UnityEngine;
using UnityEngine.UI;

public class SmarmotBarController : MonoBehaviour
{
	private Slider _bar;

	private void Start()
	{
		_bar = GetComponentInChildren<Slider>();
		_bar.value = 0;
		_bar.maxValue = 100;
		EventManager.Instance.OnQuickTimeEventStart.AddListener(HideBar);
		EventManager.Instance.OnQuickTimeSuccess.AddListener(ShowBar);
	}
	private void Update()
	{
		_bar.value = GameManager.Instance.GetSmarmotBarValue();
	}

	private void HideBar(float x)
	{
		_bar.gameObject.SetActive(false);
	}

	private void ShowBar(bool x)
	{
		_bar.gameObject.SetActive(true);
	}
}
