using ForestOfChaosLib;
using ForestOfChaosLib.AdvVar;
using ForestOfChaosLib.Attributes;
using ForestOfChaosLib.Extensions;
using ForestOfChaosLib.Maths.Curves.Components;
using ForestOfChaosLib.Maths.Lerp;
using UnityEngine;

public class CameraMotor: FoCsBehaviour
{
	[ShowAsComponent] public                  FloatReference   ScaleRef;
	public                                    FloatVariable    Damping  = 0.2f;
	[SerializeField] [DisableEditing] private Vector3          velocity = Vector3.one;
	[NoObjectFoldout]                 public  TDCurveBehaviour Curve;
	private                                   float            lerpDistance = 0;

	private void Start()
	{
		velocity = Vector3.one;
	}

	private void FixedUpdate()
	{
		lerpDistance = Lerps.Lerp(lerpDistance, ScaleRef.Value.Clamp(), Time.deltaTime);
		var pos = transform.position;
		var tD  = Curve.Lerp(lerpDistance);
		tD.Position = Vector3.SmoothDamp(pos, tD.Position, ref velocity, Damping);
		tD.ApplyData(transform);
	}
}
