using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = ButDinoConstants.NAME_ + "PowerUp/Shield")]
public class PowerUpShield: PowerUpBase
{
	private Coroutine Coroutine;
	private bool      used;

	/// <inheritdoc />
	public override void Enable(Motor motor)
	{
		StopCoroutine(motor);
		motor.OnDamageInterrupt -= DamageInterrupt;
		motor.OnDamageInterrupt += DamageInterrupt;
		//TODO: SPAWN/ENABLE ART AROUND PLAYER
		Coroutine = motor.StartCoroutine(DoLoop(motor));
	}

	private bool DamageInterrupt(WorldObject arg)
	{
		used = true;

		return false;
	}

	/// <inheritdoc />
	public override void Disable(Motor motor)
	{
		motor.OnDamageInterrupt -= DamageInterrupt;
		StopCoroutine(motor);
		motor.RemovePowerUp(this, false);
	}

	private IEnumerator DoLoop(Motor motor)
	{
		TimeRemaining.Value = TimeMax.Value;

		while(TimeRemaining.Value >= 0)
		{
			var deltaTimeRaw = Time.deltaTime;
			TimeRemaining.Value -= deltaTimeRaw;

			if(used)
			{
				used = false;

				break;
			}

			yield return null;
		}

		PowerUpDisable(motor);
	}

	private void StopCoroutine(Motor motor)
	{
		if(Coroutine != null)
			motor.StopCoroutine(Coroutine);
	}
}
