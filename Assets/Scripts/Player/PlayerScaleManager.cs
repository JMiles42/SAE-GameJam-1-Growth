using DG.Tweening;
using ForestOfChaosLib;
using ForestOfChaosLib.AdvVar;
using ForestOfChaosLib.Components;
using ForestOfChaosLib.Extensions;
using ForestOfChaosLib.Maths.Lerp;
using UnityEngine;

public class PlayerScaleManager: FoCsBehaviour
{
	public                   FloatReference    ScaleRef;
	public                   WorldSettings     WorldSettings;
	public                   FloatVariable     ScaleSpeed = 1.2f;
	[SerializeField] private OnCollisionEvents collisionEvents;

	public OnCollisionEvents OnCollisionEvents
	{
		get { return collisionEvents? collisionEvents : (collisionEvents = GetComponent<OnCollisionEvents>()); }
		set { collisionEvents = value; }
	}

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
		if(ScaleRef.Value < 20)
		{
			tween?.Complete();
			tween            = transform.DOPunchScale(Vector3.one * ScaleRef.Value.Clamp(0, 20), ScaleSpeed, 7);
			tween.onComplete -= OnComplete;
			tween.onComplete += OnComplete;
		}
		else
		{
			tween?.Complete();
			transform.DOScale(Vector3.one * ScaleRef.Value.Clamp(0, 20), 1);
		}

		WorldSettings.GameRadius = Lerps.Lerp(35f, 60f, ScaleRef.Value.Clamp(0, 2));
	}

	private void OnComplete()
	{
		transform.DOScale(Vector3.one * ScaleRef.Value, 0.2f);
	}

	private void OnCollisionEnter(Collision collision)
	{
		var worldObject = collision.gameObject.GetComponent<WorldObject>();

		if(worldObject)
			ScaleRef.Value += worldObject.ScaleIncreaseAmount;
	}
}
