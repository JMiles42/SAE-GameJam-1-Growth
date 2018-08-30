using System;
using System.Collections.Generic;
using ForestOfChaosLib;
using ForestOfChaosLib.AdvVar;
using ForestOfChaosLib.Attributes;
using ForestOfChaosLib.Components;
using ForestOfChaosLib.Extensions;
using UnityEngine;

public class Motor: FoCsRigidbodyBehaviour
{
	[GetSetter("Brain")] [SerializeField] private MotorBrain        brain;
	[SerializeField]                      private OnCollisionEvents onCollisionEvents;
	public                                        List<PowerUpBase> PowerUps = new List<PowerUpBase>();
	public                                        FloatVariable     ScaleRef;

	public MotorBrain Brain
	{
		get { return brain; }
		set
		{
#if UNITY_EDITOR
			if(Application.isPlaying)
				brain?.DisableBrain(this);
#else
			brain?.DisableBrain(this);
#endif
			brain = value;
		}
	}

	public OnCollisionEvents OnCollisionEvents
	{
		get { return onCollisionEvents? onCollisionEvents : (onCollisionEvents = GetComponent<OnCollisionEvents>()); }
		set { onCollisionEvents = value; }
	}

	private void OnEnable()
	{
		Brain?.EnableBrain(this);
		OnCollisionEvents.OnCollEnter += CollEnter;

		foreach(var powerUpBase in PowerUps)
			powerUpBase.Enable(this);
	}

	private void OnDisable()
	{
		foreach(var powerUpBase in PowerUps)
			powerUpBase.Disable(this);

		Brain?.DisableBrain(this);
		OnCollisionEvents.OnCollEnter -= CollEnter;
	}

	public event Func<WorldObject, bool> OnDamageInterrupt;

	private void CollEnter(Collision obj)
	{
		var worldObject = obj.gameObject.GetComponent<WorldObject>();

		if(worldObject && worldObject.DealsDamage.Value)
		{
			var damaged = false;

			if(OnDamageInterrupt != null)
				damaged = OnDamageInterrupt(worldObject);
			else
				damaged = true;

			if(damaged)
			{
				ScaleRef.Value = (ScaleRef.Value + worldObject.ScaleIncreaseAmount).Clamp();
				ScoreManager.AddScore(worldObject.ScoreValue);
			}
		}
		else
		{
			ScaleRef.Value = (ScaleRef.Value + worldObject.ScaleIncreaseAmount).Clamp();
			ScoreManager.AddScore(worldObject.ScoreValue);
		}
	}

	public void AddPowerUp(PowerUpBase powerUp)
	{
		PowerUps.Add(powerUp);
		powerUp.PowerUpEnable(this);
	}

	public void RemovePowerUp(PowerUpBase powerUp, bool callDisable = true)
	{
		PowerUps.Remove(powerUp);

		if(callDisable)
			powerUp.PowerUpDisable(this);
	}

#region Movement
#region Rotation
	public void SetRotation(Quaternion rot)
	{
		UpdateRotation(rot, RotationMode.Set);
	}

	public void SetRotation(Vector3 rot)
	{
		SetRotation(rot.GetQuaternion());
	}

	public void MultiplyRotation(Quaternion rot)
	{
		UpdateRotation(rot, RotationMode.Multiply);
	}

	public void MultiplyRotation(Vector3 rot)
	{
		MultiplyRotation(rot.GetQuaternion());
	}

	public void UpdateRotation(Quaternion rot, RotationMode rotationMode)
	{
		switch(rotationMode)
		{
			case RotationMode.Set:
				Rotation = rot;

				break;
			case RotationMode.Multiply:
				Rotation *= rot;

				break;
			default: throw new ArgumentOutOfRangeException(nameof(rotationMode), rotationMode, null);
		}
	}

	public enum RotationMode
	{
		Set,
		Multiply
	}
#endregion
#region Position
	public void SetPosition(Vector3 pos)
	{
		UpdatePosition(pos, PositionMode.Set);
	}

	public void AddPosition(Vector3 pos)
	{
		UpdatePosition(pos, PositionMode.Add);
	}

	public void MinusPosition(Vector3 pos)
	{
		UpdatePosition(pos, PositionMode.Minus);
	}

	public void UpdatePosition(Vector3 pos, PositionMode positionMode)
	{
		switch(positionMode)
		{
			case PositionMode.Set:
				Position = pos;

				break;
			case PositionMode.Add:
				Position += pos;

				break;
			case PositionMode.Minus:
				Position -= pos;

				break;
			default: throw new ArgumentOutOfRangeException(nameof(positionMode), positionMode, null);
		}
	}

	public enum PositionMode
	{
		Set,
		Add,
		Minus
	}
#endregion
#endregion
}
