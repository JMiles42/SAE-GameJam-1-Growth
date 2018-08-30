using System;
using System.Collections.Generic;
using ForestOfChaosLib;
using ForestOfChaosLib.AdvVar;
using UnityEngine;

public class WorldObject: FoCsRigidbodyBehaviour
{
	public static List<WorldObject> WorldObjects = new List<WorldObject>();
	public event Action<Motor>      OnPlayerInteraction;
	public bool                     RotateOnSpawn;
	public FloatVariable            ScaleIncreaseAmount = 0.1f;
	public BoolVariable             DealsDamage         = false;
	public IntVariable              ScoreValue          = 10;

	protected virtual void OnEnable()
	{
		WorldObjects.Add(this);
	}

	protected virtual  void OnDisable()
	{
		WorldObjects.Remove(this);
	}

	private void OnCollisionEnter(Collision collision)
	{
		var motor = collision.gameObject.GetComponent<Motor>();

		if(motor)
		{
			OnPlayerInteraction?.Invoke(motor);

			if(ScoreValue > 0)
				ScoreManager.AddScore(ScoreValue);

			gameObject.SetActive(false);
		}
	}
}
