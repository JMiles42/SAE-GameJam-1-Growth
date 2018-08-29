public class PowerUpMotor: WorldObject
{
	public PowerUpBase PowerUp;

	private void OnEnable()
	{
		OnPlayerInteraction += PlayerInteraction;
	}

	private void OnDisable()
	{
		OnPlayerInteraction -= PlayerInteraction;
	}

	private void PlayerInteraction(Motor motor)
	{
		motor.AddPowerUp(PowerUp);
	}
}
