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
	private bool                    interactedWithPlayer = false;

	protected virtual void OnEnable()
	{
		WorldObjects.Add(this);
		interactedWithPlayer = false;
	}

	protected virtual void OnDisable()
	{
		WorldObjects.Remove(this);
	}

	private void OnCollisionEnter(Collision collision)
	{
		var motor = collision.gameObject.GetComponent<Motor>();

		if(motor && !interactedWithPlayer)
		{
			interactedWithPlayer = true;
			OnPlayerInteraction?.Invoke(motor);

			if(ScoreValue != 0)
				ScoreManager.AddScore(ScoreValue);

			gameObject.SetActive(false);
		}
	}
}
