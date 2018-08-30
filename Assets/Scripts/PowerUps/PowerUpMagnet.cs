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
			var deltaTime     = Time.deltaTime;
			var deltaTimeLoop = deltaTime * MagnetStrength;
			var motorPos      = motor.Position;

			for(var i = hits.Length - 1; i >= 0; i--)
			{
				var hit         = hits[i];
				var worldObject = hit.transform.GetComponent<WorldObject>();

				if(!worldObject)
					continue;

				if(!worldObject.DealsDamage)
				{
					if(worldObject.Position.Distance(motor.Position) <= 10)
					{
						worldObject.Position = Vector3.Lerp(worldObject.Position, motorPos, deltaTimeLoop * 3);
					}
					else if(worldObject.Position.Distance(motor.Position) <= 5)
					{
						motor.UsePickup(worldObject);
						worldObject.gameObject.SetActive(false);
					}
					else
					{
						worldObject.Position = Vector3.Lerp(worldObject.Position, motorPos, deltaTimeLoop);
					}
				}

			}

			TimeRemaining.Value -= deltaTime;

			yield return null;
		}

		PowerUpDisable(motor);
	}
}
