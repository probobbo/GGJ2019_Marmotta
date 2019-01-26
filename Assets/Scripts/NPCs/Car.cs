using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : AbstractNPC
{
	[SerializeField] private Vector3 _start, _end;
	private bool _isOnStart;
	public float TimeToComplete = 5.0f;

	protected new void Start()
	{
		base.Start();
		_isOnStart = true;
		transform.position = _start;
	}

	protected override void Move()
	{
		Vector3 vel = new Vector3(0, 0, 0);
		float dist = 0;
		if (_isOnStart)
		{
			dist = Vector3.Distance(transform.position, _end);
			Vector3.SmoothDamp(_start, _end, ref vel, TimeToComplete);
		}
		else
		{
			dist = Vector3.Distance(transform.position, _start);
			Vector3.SmoothDamp(_end, _start, ref vel, TimeToComplete);
		}
		if (dist <= 0.5)
		{
			_isOnStart = !_isOnStart;
			transform.Rotate(axis: Vector3.up, angle: 180);
		}
		transform.position += vel;
	}
}
