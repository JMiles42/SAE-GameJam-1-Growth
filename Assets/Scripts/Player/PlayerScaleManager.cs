using DG.Tweening;
using ForestOfChaosLib;
using ForestOfChaosLib.AdvVar;
using ForestOfChaosLib.Components;
using ForestOfChaosLib.Extensions;
using ForestOfChaosLib.Maths.Lerp;
using UnityEngine;

public class PlayerScaleManager: FoCsBehaviour
{
	public FloatReference ScaleRef;
	public WorldSettings  WorldSettings;

	private void OnEnable()
	{
		ScaleRef.Value         =  0;
		ScaleRef.OnValueChange += ValueChange;
	}

	private void OnDisable()
	{
		ScaleRef.OnValueChange -= ValueChange;
	}

	private void ValueChange()
	{
		var value = ScaleRef.Value.Clamp();
		transform.localScale     = Vector3.Lerp(transform.localScale, Vector3.one * value, Time.deltaTime);
		WorldSettings.GameRadius = Lerps.Lerp(35f, 120f, value);
	}
}
