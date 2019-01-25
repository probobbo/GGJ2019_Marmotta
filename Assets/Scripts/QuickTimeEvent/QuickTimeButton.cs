using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class QuickTimeButton : MonoBehaviour
{
	private Image _buttonImage;
	private QuickTimeBar _quickTimeBar;
	private Vector3 _targetPosition;
	private InputManager.ControllerButtons _buttonType;
	private float _width = 0.0f;
	public Sprite[] ButtonsImages;

    // Start is called before the first frame update
    void Start()
    {
		_buttonImage = GetComponent<Image>();
		_quickTimeBar = GetComponentInParent<QuickTimeBar>();
		_buttonType  = (InputManager.ControllerButtons)UnityEngine.Random.Range(0, Enum.GetValues(typeof( InputManager.ControllerButtons)).Length);
		int index = (int)_buttonType;
		if (index < ButtonsImages.Length)
		{
			_buttonImage.sprite = ButtonsImages[index];
		}
		_width = GetComponent<RectTransform>().sizeDelta.x;
	}

	public void SwipeLeft()
	{
		transform.position += new Vector3(-_width, 0, 0);
	}

    public InputManager.ControllerButtons GetButtonType()
	{
		return _buttonType;
	}
}
