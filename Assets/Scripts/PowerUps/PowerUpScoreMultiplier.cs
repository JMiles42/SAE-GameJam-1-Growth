using System.Collections;
using ForestOfChaosLib.AdvVar;
using UnityEngine;

[CreateAssetMenu(menuName = ButDinoConstants.NAME_ + "PowerUp/Score Multiplier")]
public class PowerUpScoreMultiplier: PowerUpBase
{
	public  IntVariable   Multiplier = 2;
	public  FloatVariable TimeActive = 20;
	public  float         time       = 20;
	private Coroutine     countdown;

	/// <inheritdoc />
	public override void PowerUpEnable(Motor motor)
	{
		ScoreManager.OnScoreInterrupt -= OnScoreInterrupt;
		ScoreManager.OnScoreInterrupt += OnScoreInterrupt;

		if(countdown == null)
			countdown = motor.StartCoroutine(Countdown());
	}

	private IEnumerator Countdown()
	{
		while(time >= 0)
		{
			TimeActive.Value -= Time.deltaTime;

			yield return null;
		}
	}

	/// <inheritdoc />
	public override void PowerUpDisable(Motor motor)
	{
		ScoreManager.OnScoreInterrupt -= OnScoreInterrupt;
	}

	private int OnScoreInterrupt(int arg) => arg * Multiplier;
}
