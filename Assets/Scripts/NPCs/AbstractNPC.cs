using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Managers;

public abstract class AbstractNPC : MonoBehaviour
{
	[SerializeField] private Vector3 _offsetToPlayer;
	[SerializeField] private float _npcTweenDuration = 0.5f;
	private DialogManager _dialogManager;
	private bool wasHit = false;

	private void Start()
	{
		_dialogManager = GetComponentInChildren<DialogManager>(true);
	}

	private void Update()
	{
		Move();
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player") && !wasHit)
		{
			EventManager.Instance.OnPlayingStateChanged.Invoke(GameManager.PlayingState.Dialoguing);
			_dialogManager.StartDialog();
			transform.DOMoveX(other.transform.position.x + _offsetToPlayer.x, _npcTweenDuration);
			transform.DOMoveZ(other.transform.position.z + _offsetToPlayer.z, _npcTweenDuration);
			Vector3 pointToLookAt = new Vector3(other.transform.position.x, transform.position.y, other.transform.position.z);
			transform.DORotate(pointToLookAt, _npcTweenDuration);
			wasHit = true;
		}
	}

	protected abstract void Move();
}
