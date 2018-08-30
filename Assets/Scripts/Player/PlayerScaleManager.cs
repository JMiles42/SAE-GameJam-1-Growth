using ForestOfChaosLib;
using ForestOfChaosLib.AdvVar;
using ForestOfChaosLib.Extensions;
using ForestOfChaosLib.Maths.Lerp;
using UnityEngine;

public class PlayerScaleManager: FoCsBehaviour
{
	public FloatReference ScaleRef;
	public WorldSettings  WorldSettings;
	public FloatVariable  ScaleSpeed = 1.5f;

	private void Start()
	{
		ScaleRef.Value           = 0;
		WorldSettings.GameRadius = WorldSettings.StartRadius;
	}

	private void Update()
	{
		var deltaTime = Time.deltaTime;
		var value     = ScaleRef.Value.Clamp(0.2f);
		transform.localScale     = Vector3.Lerp(transform.localScale, Vector3.one * value, deltaTime * ScaleSpeed);
		WorldSettings.GameRadius = Lerps.Lerp(WorldSettings.StartRadius, 120f, ScaleRef.Value);
	}
}
