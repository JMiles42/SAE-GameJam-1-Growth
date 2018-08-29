using System;
using ForestOfChaosLib;
using UnityEngine;

public class WorldObject: FoCsRigidbodyBehaviour
{
	public event Action<Motor> OnPlayerInteraction;

	private void OnCollisionEnter(Collision collision)
	{
		var motor = collision.gameObject.GetComponent<Motor>();

		if(motor)
			OnPlayerInteraction?.Invoke(motor);
	}
}
