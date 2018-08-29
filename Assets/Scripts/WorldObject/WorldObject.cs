using System;
using ForestOfChaosLib;
using ForestOfChaosLib.AdvVar;
using UnityEngine;

public class WorldObject: FoCsRigidbodyBehaviour
{
	public event Action<Motor> OnPlayerInteraction;
	public bool                RotateOnSpawn;
	public FloatVariable       ScaleIncreaseAmount = 0.1f;

	private void OnCollisionEnter(Collision collision)
	{
		var motor = collision.gameObject.GetComponent<Motor>();

		if(motor)
		{
			OnPlayerInteraction?.Invoke(motor);
			gameObject.SetActive(false);
		}
	}
}
