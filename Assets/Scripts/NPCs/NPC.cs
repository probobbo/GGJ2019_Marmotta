using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : AbstractNPC
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
			if (!_walking)
			{
				_anim.SetTrigger("walk");
				_walking = true;
			}
			Vector3 dir = (_player.position - transform.position).normalized;
			transform.LookAt(_player);
			transform.position += dir * Speed * Time.deltaTime;
		}
	}
}
