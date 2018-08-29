using System.Collections;
using ForestOfChaosLib.AdvVar;
using UnityEngine;

[CreateAssetMenu(menuName = ButDinoConstants.NAME_ + "PowerUp/Score Multiplier")]
public class PowerUpScoreMultiplier: PowerUpBase
{
	public  IntVariable Multiplier = 2;
	private Coroutine   countdown;

	/// <inheritdoc />
	public override void Enable(Motor motor)
	{
		ScoreManager.OnScoreInterrupt -= OnScoreInterrupt;
		ScoreManager.OnScoreInterrupt += OnScoreInterrupt;

		if(countdown == null)
			countdown = motor.StartCoroutine(Countdown(motor));
	}

	private IEnumerator Countdown(Motor motor)
	{
		TimeRemaining.Value = TimeMax.Value;

		while(TimeRemaining.Value >= 0)
		{
			TimeRemaining.Value -= Time.deltaTime;

			yield return null;
		}

		PowerUpDisable(motor);
	}

	/// <inheritdoc />
	public override void Disable(Motor motor)
	{
		ScoreManager.OnScoreInterrupt -= OnScoreInterrupt;
		motor.RemovePowerUp(this, false);
	}

	private int OnScoreInterrupt(int arg) => arg * Multiplier;
}
