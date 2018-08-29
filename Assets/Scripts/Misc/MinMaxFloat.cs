using System;
using ForestOfChaosLib.Attributes;
using UnityEngine;

[Serializable]
public struct MinMaxFloat
{
	[Half10Line] public float Min;
	[Half01Line] public float Max;
	public float Lerp(float time) => Mathf.Lerp(Min, Max, time);
	public float Random() => UnityEngine.Random.Range(Min, Max);
	public Vector2 RandomV2() => new Vector2(Random(), Random());
	public Vector3 RandomV3() => new Vector3(Random(), Random(), Random());
}
