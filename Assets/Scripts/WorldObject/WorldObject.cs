using System;
using System.Collections.Generic;
using ForestOfChaosLib;
using ForestOfChaosLib.AdvVar;
using UnityEngine;

public class WorldObject: FoCsRigidbodyBehaviour
{
	public static List<WorldObject> WorldObjects = new List<WorldObject>();
	public        BoolVariable      DealsDamage  = false;
	public        bool              RotateOnSpawn;
	public        FloatVariable     ScaleIncreaseAmount = 0.1f;
	public        IntVariable       ScoreValue          = 10;
	public event Action<Motor>      OnPlayerInteraction;
	public bool                     interactedWithPlayer         = false;
	public bool                     internalInteractedWithPlayer = false;

	protected virtual void OnEnable()
	{
		WorldObjects.Add(this);
		interactedWithPlayer = internalInteractedWithPlayer = false;
	}

	protected virtual void OnDisable()
	{
		WorldObjects.Remove(this);
	}

	private void OnCollisionEnter(Collision collision)
	{
		var motor = collision.gameObject.GetComponent<Motor>();

		if(!motor)
			return;

		if(!internalInteractedWithPlayer)
		{
			internalInteractedWithPlayer = true;
			OnPlayerInteraction?.Invoke(motor);
		}
		gameObject.SetActive(false);
	}
}
