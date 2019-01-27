using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Police : AbstractNPC
{

	private Animator _anim;
	private Transform _player;
	private bool _walking = false;
	public float Speed, Range;

	private new void Start()
	{
		base.Start();
		_player = GameObject.FindGameObjectWithTag("Player").transform;
		_anim = GetComponentInChildren<Animator>();
	}

	protected override void Move()
	{
		if (_walking || Vector3.Distance(transform.position, _player.position) <= Range)
		{
			if (!_walking) {
				_anim.SetTrigger("walk");
				_walking = true;
			}
			transform.LookAt(_player);
		}
	}
}
