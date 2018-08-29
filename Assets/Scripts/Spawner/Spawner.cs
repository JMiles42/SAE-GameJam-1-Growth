using System.Collections;
using ForestOfChaosLib.AdvVar;
using ForestOfChaosLib.Attributes;
using UnityEngine;

public class Spawner: MonoBehaviour
{
	public             MotorRTRef    Player;
	public             Pool[]        Spawnables;
	public             BoolVariable  Spawning = true;
	[NoFoldout] public MinMaxFloat   Timer;
	public             WorldSettings WorldSettings;
	private void OnDisable() { }

	private void Start()
	{
		StartCoroutine(DoSpawnLoop());
	}

	private IEnumerator DoSpawnLoop()
	{
		while(Spawning.Value)
			yield return new WaitForSeconds(Timer.Random());
		//Spawnables.UnityRandomObject().
	}
}
