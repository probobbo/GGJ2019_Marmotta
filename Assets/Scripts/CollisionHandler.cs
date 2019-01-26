using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
	private PlayerController _player;

    // Start is called before the first frame update
    void Start()
    {
		_player = GetComponentInParent<PlayerController>();
    }

	private void OnTriggerEnter(Collider other)
	{
		_player.OnTriggerEnter(other);
	}
}
