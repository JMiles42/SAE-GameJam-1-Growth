using System.Collections;
using DG.Tweening;
using ForestOfChaosLib;
using ForestOfChaosLib.AdvVar;
using ForestOfChaosLib.Maths.Lerp;
using UnityEngine;

public class PlayerScaleManager: FoCsBehaviour
{
	public FloatReference ScaleRef;
	public WorldSettings  WorldSettings;
	public FloatVariable  ScaleSpeed = 1.2f;

	private void OnEnable()
	{
		ScaleRef.Value         =  1;
		ScaleRef.OnValueChange += ValueChange;
	}

	private void OnDisable()
	{
		ScaleRef.OnValueChange -= ValueChange;
	}

	private Tweener tween;

	private void ValueChange()
	{
		if(tween != null)
			tween.Complete();
		else
			tween = transform.DOPunchScale(Vector3.one * ScaleRef.Value, ScaleSpeed, 6, 0.3f);

		WorldSettings.GameRadius = Lerps.Lerp(50f, 80f, ScaleRef.Value);
	}

	private void OnCollisionEnter(Collision collision)
	{
		var worldObject = collision.gameObject.GetComponent<WorldObject>();

		if(worldObject)
			ScaleRef.Value += worldObject.ScaleIncreaseAmount;
	}
}
