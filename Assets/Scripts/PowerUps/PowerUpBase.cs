using UnityEngine;

public abstract class PowerUpBase : ScriptableObject {
	public abstract void EnableBrain(Motor  motor);
	public abstract void DisableBrain(Motor motor);
}
