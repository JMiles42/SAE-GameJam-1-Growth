using System.Collections;
using System.Collections.Generic;
using ForestOfChaosLib;
using ForestOfChaosLib.AdvVar;
using ForestOfChaosLib.Attributes;
using ForestOfChaosLib.Maths.Lerp;
using UnityEngine;

public class WorldObjectMover: FoCsBehaviour
{
	private const           float         LERP_TIME_THRESHOLD = 0.98f;
	private                 Coroutine     Coroutine;
	public                  FloatVariable LerpSpeed;
	[DisableEditing] public List<Vector3> list;
	private                 Vector3       position;
	public                  float         PositionScale = 5;

	private void OnEnable()
	{
		position = transform.position;
		StopCoroutine();
		Coroutine = StartCoroutine(DoTransformAnimation());
	}

	private void Start()
	{
		position = transform.position;
	}

	private void OnDisable()
	{
		StopCoroutine();
	}

	private void StopCoroutine()
	{
		if(Coroutine != null)
			StopCoroutine(Coroutine);
	}

	private IEnumerator DoTransformAnimation()
	{
		position = transform.position;
		list     = NewList();
		var lerpTime = 0f;
		var vel      = Vector3.zero;

		while(gameObject.activeSelf)
		{
			var deltaTime = Time.deltaTime;
			lerpTime = lerpTime + (LerpSpeed * deltaTime);

			yield return null;

			if(lerpTime >= LERP_TIME_THRESHOLD)
			{
				lerpTime -= 1;
				list     =  NewList();
			}
			else
				list[0] = transform.position;

			var lerpPos = Vector3Lerp.Lerp(list, lerpTime);
			transform.position = lerpPos;
		}
	}

	private List<Vector3> NewList()
	{
		var pos     = transform.position;
		var rtnVal  = new List<Vector3> {pos};
		var lastVal = pos;

		for(var i = 0; i < 19; i++)
			rtnVal.Add(lastVal = lastVal + (Random.onUnitSphere * PositionScale));

		return rtnVal;
	}
}
