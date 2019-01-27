﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Managers;

public abstract class AbstractNPC : MonoBehaviour
{
	[SerializeField] private Vector3 _offsetToPlayer;
	[SerializeField] private Transform _cameraOffset;
	[SerializeField] private float _npcTweenDuration = 0.5f;
	private DialogManager _dialogManager;
	private Animator _animator;
	private Transform _cameraTransform;
	protected bool _wasHit = false;
	private float _cameraTweenDuration = 0.5f;

	protected void Start()
	{
		_dialogManager = GetComponentInChildren<DialogManager>(true);
		_animator = GetComponentInChildren<Animator>(true);
		_cameraTransform = Camera.main.transform;
	}

	private void Update()
	{
		if (!_wasHit)
			Move();
	}

	public void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player") && !_wasHit && GameManager.Instance.CanChangeState)
		{
			EventManager.Instance.OnPlayingStateChanged.Invoke(GameManager.PlayingState.Dialoguing);
			_dialogManager.StartDialog();
			if (_animator != null)
				_animator.SetTrigger("talk");
			_cameraTransform.DOMove(_cameraOffset.position, _cameraTweenDuration);
			_cameraTransform.DORotate(_cameraOffset.rotation.eulerAngles, _cameraTweenDuration);
			/*transform.DOMoveX(other.transform.position.x + _offsetToPlayer.x, _npcTweenDuration);
			transform.DOMoveZ(other.transform.position.z + _offsetToPlayer.z, _npcTweenDuration);
			Vector3 pointToLookAt = new Vector3(other.transform.position.x, transform.position.y, other.transform.position.z);
			*///transform.DORotate(other.transform.position, _npcTweenDuration).OnComplete(()=> transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0));
			transform.DOLookAt(other.transform.position, _npcTweenDuration);
			_wasHit = true;
		}
	}

	protected abstract void Move();
}
