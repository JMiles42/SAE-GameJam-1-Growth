using System.Collections;
using ForestOfChaosLib.AdvVar;
using ForestOfChaosLib.AdvVar.RuntimeRef;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = ButDinoConstants.NAME_ + "PowerUp/Score Multiplier")]
public class PowerUpScoreMultiplier: PowerUpBase
{
	private Coroutine            countdown;
	public  GameObjectRunTimeRef Icon;
	public  IntVariable          Multiplier = 2;
	private Image                multiplierIcon;

	/// <inheritdoc />
	public override void Enable(Motor motor)
	{
		ScoreManager.OnScoreInterrupt -= OnScoreInterrupt;
		ScoreManager.OnScoreInterrupt += OnScoreInterrupt;
		multiplierIcon                =  Icon.Reference.GetComponent<Image>();
		multiplierIcon.enabled        =  true;

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
		multiplierIcon.enabled = false;
	}

	private int OnScoreInterrupt(int arg) => arg * Multiplier;
}
