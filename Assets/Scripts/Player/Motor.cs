using System;
using System.Collections.Generic;
using ForestOfChaosLib;
using ForestOfChaosLib.Attributes;
using ForestOfChaosLib.Extensions;
using UnityEngine;

public class Motor: FoCsRigidbodyBehaviour
{
	[GetSetter("Brain")] [SerializeField] private MotorBrain        brain;
	public                                        List<PowerUpBase> PowerUps = new List<PowerUpBase>();

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

	private void OnEnable()
	{
		Brain?.EnableBrain(this);
	}

	private void OnDisable()
	{
		Brain?.DisableBrain(this);
	}

	public void AddPowerUp(PowerUpBase powerUp)
	{
		PowerUps.Add(powerUp);
		powerUp.PowerUpEnable(this);
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
