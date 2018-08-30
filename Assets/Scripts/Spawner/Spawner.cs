using System.Collections;
using ForestOfChaosLib.AdvVar;
using ForestOfChaosLib.Attributes;
using ForestOfChaosLib.Extensions;
using UnityEngine;

public class Spawner: MonoBehaviour
{
	public const       float             SPAWN_DISTANCE = 100;
	public             WorldObjectPool[] DangerousSpawnables;
	[NoFoldout] public MinMaxFloat       DangerousTimer;
	public             MotorRTRef        Player;
	public             WorldObjectPool[] PowerUpSpawnables;
	[NoFoldout] public MinMaxFloat       PowerUpTimer;
	public             WorldObjectPool[] SafeSpawnables;
	[NoFoldout] public MinMaxFloat       SafeTimer;
	public             BoolVariable      Spawning = true;
	public             WorldSettings     WorldSettings;
	private void OnDisable() { }

	private void Start()
	{
		StartCoroutine(DoSpawnLoop());
	}

	private void DoInit()
	{
		for(var i = 10; i < 100; i += 4)
			RandomSpawn(i);
	}

	private void RandomSpawn(float zDist)
	{
		DoSpawnInWorld(Random.value >= 0.5f? DangerousSpawnables.UnityRandomObject().Next : SafeSpawnables.UnityRandomObject().Next, zDist);
	}

	private IEnumerator DoSpawnLoop()
	{
		yield return null;

		DoInit();

		yield return null;

		var powerUpTimer     = PowerUpTimer.Random();
		var dangerousTimer   = DangerousTimer.Random();
		var normalSpawnTimer = SafeTimer.Random();

		while(Spawning.Value)
		{
			var deltaTime = Time.deltaTime;
			powerUpTimer     -= deltaTime;
			normalSpawnTimer -= deltaTime;
			dangerousTimer   -= deltaTime;

			if(normalSpawnTimer <= 0)
			{
				DoSpawnInWorld(SafeSpawnables.UnityRandomObject().Next);
				normalSpawnTimer = SafeTimer.Random();
			}

			if(powerUpTimer <= 0)
			{
				DoSpawnInWorld(PowerUpSpawnables.UnityRandomObject().Next);
				powerUpTimer = PowerUpTimer.Random();
			}

			if(dangerousTimer <= 0)
			{
				DoSpawnInWorld(DangerousSpawnables.UnityRandomObject().Next);
				dangerousTimer = DangerousTimer.Random();
			}

			yield return null;
		}
	}

	private void DoSpawnInWorld(WorldObject worldObject, float zDist = SPAWN_DISTANCE)
	{
		var num = WorldSettings.BoundsRadius;
		var pos = Random.insideUnitCircle * num;

		if(worldObject.RotateOnSpawn)
			worldObject.transform.rotation = Random.rotation;

		worldObject.transform.position = new Vector3(pos.x, pos.y, Player.Reference.Position.z + zDist);
		worldObject.gameObject.SetActive(true);
	}
}
