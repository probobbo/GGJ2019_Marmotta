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
	private Animator _animator;
	private bool wasHit = false;

	protected void Start()
	{
		_dialogManager = GetComponentInChildren<DialogManager>(true);
		_animator = GetComponentInChildren<Animator>(true);
	}

	private void Update()
	{
		if (!wasHit)
			Move();
	}

	public void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player") && !wasHit)
		{
			EventManager.Instance.OnPlayingStateChanged.Invoke(GameManager.PlayingState.Dialoguing);
			_dialogManager.StartDialog();
			_animator.SetTrigger("talk");
			/*transform.DOMoveX(other.transform.position.x + _offsetToPlayer.x, _npcTweenDuration);
			transform.DOMoveZ(other.transform.position.z + _offsetToPlayer.z, _npcTweenDuration);
			Vector3 pointToLookAt = new Vector3(other.transform.position.x, transform.position.y, other.transform.position.z);
			*///transform.DORotate(other.transform.position, _npcTweenDuration).OnComplete(()=> transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0));
			transform.DOLookAt(other.transform.position, _npcTweenDuration);
			wasHit = true;
		}
	}

	protected abstract void Move();
}
