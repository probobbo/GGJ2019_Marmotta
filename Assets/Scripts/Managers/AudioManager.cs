using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
	public static AudioManager Instance;

	private void Awake()
	{
		if (!Instance)
		{
			Instance = this;
		}
		else if (Instance != this)
			Destroy(gameObject);
	}

	// Start is called before the first frame update
	private void Start()
	{

	}

	// Update is called once per frame
	private void Update()
	{

	}
}
