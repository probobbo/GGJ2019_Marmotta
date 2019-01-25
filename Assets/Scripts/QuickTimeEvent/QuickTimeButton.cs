using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class QuickTimeButton : MonoBehaviour
{
	private Image _buttonImage;
	private QuickTimeBar _quickTimeBar;
	private Vector3 _targetPosition;

    // Start is called before the first frame update
    void Start()
    {
		_buttonImage = GetComponent<Image>();
		_quickTimeBar = GetComponentInParent<QuickTimeBar>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
