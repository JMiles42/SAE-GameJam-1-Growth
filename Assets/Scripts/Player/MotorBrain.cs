using UnityEngine;

public abstract class MotorBrain: ScriptableObject
{
	public abstract void EnableBrain(Motor  motor);
	public abstract void DisableBrain(Motor motor);
}
