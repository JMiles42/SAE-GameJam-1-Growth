using System;
using ForestOfChaosLib.AdvVar;
using UnityEngine;

public abstract class PowerUpBase: ScriptableObject
{
	public FloatVariable TimeMax       = 20;
	public FloatVariable TimeRemaining = 20;
	public event Action  OnEnabled;
	public event Action  OnDisabled;
	public abstract void Enable(Motor  motor);
	public abstract void Disable(Motor motor);

	public void PowerUpEnable(Motor motor)
	{
		Enable(motor);
		OnEnabled?.Invoke();
	}

	public void PowerUpDisable(Motor motor)
	{
		OnDisabled?.Invoke();
		Disable(motor);
	}
}
