using UnityEngine;

public abstract class PowerUpBase: ScriptableObject
{
	public abstract void PowerUpEnable(Motor  motor);
	public abstract void PowerUpDisable(Motor motor);
}
