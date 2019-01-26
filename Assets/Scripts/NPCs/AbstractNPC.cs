using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractNPC : MonoBehaviour
{
	private bool wasHit = false;

	private void Update()
	{
		Move();
	}

	private void OnTriggerEnter(Collider other)
	{
		Debug.Log("ote");
		if (other.CompareTag("Player") && !wasHit)
		{
			Debug.Log("hit player");
			EventManager.Instance.OnPlayingStateChanged.Invoke(GameManager.PlayingState.Dialoguing);
			wasHit = true;
		}
	}

	protected abstract void Move();
}
