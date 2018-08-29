using System;
using ForestOfChaosLib.Attributes;
using ForestOfChaosLib.Extensions;
using UnityEngine;

[Serializable]
public struct MinMaxInt
{
	[Half10Line] public int Min;
	[Half01Line] public int Max;

	public int Lerp(float time) => Mathf.Lerp(Min, Max, time).CastToInt();

	public int Random() => UnityEngine.Random.Range(Min, Max);
}
