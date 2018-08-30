using ForestOfChaosLib.Attributes;

public class PowerUpMotor: WorldObject
{
	[ShowAsComponent] public PowerUpBase PowerUp;

	protected override void OnEnable()
	{
		base.OnEnable();
		OnPlayerInteraction += PlayerInteraction;
	}

	protected override void OnDisable()
	{
		base.OnDisable();
		OnPlayerInteraction -= PlayerInteraction;
	}

	private void PlayerInteraction(Motor motor)
	{
		motor.AddPowerUp(PowerUp);
	}
}
