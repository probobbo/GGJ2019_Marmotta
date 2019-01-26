using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCCollisionHandler : MonoBehaviour
{
	private AbstractNPC _npc;

	// Start is called before the first frame update
	void Start()
	{
		_npc = GetComponentInParent<AbstractNPC>();
	}

	private void OnTriggerEnter(Collider other)
	{
		_npc.OnTriggerEnter(other);
	}
}