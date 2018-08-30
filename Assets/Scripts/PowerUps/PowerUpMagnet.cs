using System.Collections;
using ForestOfChaosLib.AdvVar;
using ForestOfChaosLib.AdvVar.RuntimeRef;
using ForestOfChaosLib.Extensions;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = ButDinoConstants.NAME_ + "PowerUp/Magnet")]
public class PowerUpMagnet: PowerUpBase
{
	private Coroutine            Coroutine;
	public  GameObjectRunTimeRef Icon;
	private Image                magnetIcon;
	public  FloatVariable        MagnetRadius   = 20;
	public  FloatVariable        MagnetStrength = 2;

	/// <inheritdoc />
	public override void Enable(Motor motor)
	{
		StopCoroutine(motor);
		Coroutine          = motor.StartCoroutine(DoLoop(motor));
		magnetIcon         = Icon.Reference.GetComponent<Image>();
		magnetIcon.enabled = true;
	}

	/// <inheritdoc />
	public override void Disable(Motor motor)
	{
		StopCoroutine(motor);
		motor.RemovePowerUp(this, false);
		if(magnetIcon)
		magnetIcon.enabled = false;
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
			var deltaTime  = Time.deltaTime;
			var deltaTimeLoop = deltaTime * (MagnetStrength);

			foreach(var hit in hits)
			{
				var worldObject = hit.transform.GetComponent<WorldObject>();

				if(worldObject && !worldObject.DealsDamage)
					worldObject.Position = Vector3.Lerp(worldObject.Position, motor.Position, deltaTimeLoop);
			}

			TimeRemaining.Value -= deltaTime;

			yield return null;
		}

		PowerUpDisable(motor);
	}
}
