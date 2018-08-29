using System.Collections;
using ForestOfChaosLib.AdvVar;
using ForestOfChaosLib.Attributes;
using ForestOfChaosLib.Extensions;
using UnityEngine;

public class Spawner: MonoBehaviour
{
	public             MotorRTRef        Player;
	public             WorldObjectPool[] Spawnables;
	public             WorldObjectPool[] PowerUpSpawnables;
	public             BoolVariable      Spawning = true;
	[NoFoldout] public MinMaxFloat       PowerUpTimer;
	[NoFoldout] public MinMaxFloat       Timer;
	public             WorldSettings     WorldSettings;
	public const       float             SPAWN_DISTANCE = 100;
	private void OnDisable() { }

	private void Start()
	{
		StartCoroutine(DoSpawnLoop());
	}

	private IEnumerator DoSpawnLoop()
	{
		yield return null;

		var powerUpTimer     = PowerUpTimer.Random();
		var normalSpawnTimer = Timer.Random();

		while(Spawning.Value)
		{
			var deltaTime = Time.deltaTime;
			powerUpTimer     -= deltaTime;
			normalSpawnTimer -= deltaTime;

			if(normalSpawnTimer <= 0)
			{
				DoSpawnInWorld(Spawnables.UnityRandomObject().Next);
				normalSpawnTimer = Timer.Random();
			}

			if(powerUpTimer <= 0)
			{
				DoSpawnInWorld(PowerUpSpawnables.UnityRandomObject().Next);
				powerUpTimer = PowerUpTimer.Random();
			}

			yield return null;
		}
	}

	private void DoSpawnInWorld(WorldObject worldObject)
	{
		var num = WorldSettings.BoundsRadius;
		var pos = Random.insideUnitCircle * num;

		if(worldObject.RotateOnSpawn)
			worldObject.transform.rotation = Random.rotation;

		worldObject.transform.position = new Vector3(pos.x, pos.y, Player.Reference.Position.z + SPAWN_DISTANCE);
		worldObject.gameObject.SetActive(true);
	}
}
