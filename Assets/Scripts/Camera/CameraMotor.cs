using ForestOfChaosLib;
using ForestOfChaosLib.AdvVar;
using ForestOfChaosLib.Maths.Lerp;
using UnityEngine;
using UnityEngine.Animations;

public class CameraMotor: FoCsBehaviour
{
	public  Vector3            Offset = new Vector3(0, 3, -7);
	public  PositionConstraint PositionConstraint;
	public  FloatVariable      ScaleRef;
	public  Transform          Target;
	private Vector3            velocity;

	private void Start()
	{
		Offset = transform.position - Target.position;
	}

	private void LateUpdate()
	{
		PositionConstraint.translationOffset = Vector3Lerp.Lerp(Target.position, Target.position + Offset, ScaleRef.Value);
	}
}
