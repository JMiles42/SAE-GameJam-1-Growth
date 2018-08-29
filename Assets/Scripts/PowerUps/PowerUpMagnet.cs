using System.Collections;
using ForestOfChaosLib.AdvVar;
using UnityEngine;

[CreateAssetMenu(menuName = ButDinoConstants.NAME_ + "PowerUp/Magnet")]
public class PowerUpMagnet: PowerUpBase
{
	public  FloatVariable MagnetRadius   = 20;
	public  FloatVariable MagnetStrength = 2;
	private Coroutine     Coroutine;

	/// <inheritdoc />
	public override void Enable(Motor motor)
	{
		StopCoroutine(motor);
		Coroutine = motor.StartCoroutine(DoLoop(motor));
	}

	/// <inheritdoc />
	public override void Disable(Motor motor)
	{
		StopCoroutine(motor);
		motor.RemovePowerUp(this, false);
	}

	private void StopCoroutine(Motor motor)
	{
		if(Coroutine != null)
			motor.StopCoroutine(Coroutine);
	}

	private IEnumerator DoLoop(Motor motor)
	{
		TimeRemaining.Value = TimeMax.Value;

		while(TimeRemaining.Value >= 0)
		{
			var ray           = new Ray(motor.Position, motor.Forward);
			var hits          = Physics.SphereCastAll(ray, MagnetRadius);
			var deltaTimeRaw  = Time.deltaTime;
			var deltaTimeLoop = deltaTimeRaw * MagnetStrength;

			foreach(var hit in hits)
			{
				var worldObject = hit.transform.GetComponent<WorldObject>();

				if(worldObject && !worldObject.DealsDamage)
					worldObject.Position = Vector3.Lerp(worldObject.Position, motor.Position, deltaTimeLoop);
			}

			TimeRemaining.Value -= deltaTimeRaw;

			yield return null;
		}

		PowerUpDisable(motor);
	}
}
