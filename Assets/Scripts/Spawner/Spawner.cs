using System.Collections;
using ForestOfChaosLib.AdvVar;
using ForestOfChaosLib.Attributes;
using ForestOfChaosLib.Extensions;
using UnityEngine;

public class Spawner: MonoBehaviour
{
	public             MotorRTRef    Player;
	public             BoolVariable  Spawning = true;
	public             WorldSettings WorldSettings;
	[NoFoldout] public MinMaxFloat   Timer;
	public             Pool[]        Spawnables;
	private void OnDisable() { }

	private void Start()
	{
		StartCoroutine(DoSpawnLoop());
	}

	private IEnumerator DoSpawnLoop()
	{
		while(Spawning.Value)
		{
			yield return new WaitForSeconds(Timer.Random());
			//Spawnables.UnityRandomObject().
		}
	}
}
