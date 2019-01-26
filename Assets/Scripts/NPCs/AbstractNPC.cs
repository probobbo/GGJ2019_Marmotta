using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public abstract class AbstractNPC : MonoBehaviour
{
	[SerializeField] private Vector3 _offsetToPlayer;
	[SerializeField] private float _npcTweenDuration = 0.5f;
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
			transform.DOMoveX(other.transform.position.x + _offsetToPlayer.x, _npcTweenDuration);
			transform.DOMoveZ(other.transform.position.z + _offsetToPlayer.z, _npcTweenDuration);
			Vector3 pointToLookAt = new Vector3(other.transform.position.x, transform.position.y, other.transform.position.z);
			transform.DORotate(pointToLookAt, _npcTweenDuration);
			wasHit = true;
		}
	}

	protected abstract void Move();
}
