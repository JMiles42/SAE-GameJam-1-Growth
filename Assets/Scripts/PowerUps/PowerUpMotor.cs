using System.Collections;
using System.Collections.Generic;
using ForestOfChaosLib;
using UnityEngine;

public class PowerUpMotor: WorldObject
{
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
		
	}
}
