using System.Collections;
using ForestOfChaosLib.AdvVar;
using ForestOfChaosLib.AdvVar.InputSystem;
using UnityEngine;

[CreateAssetMenu]
public class PlayerBrain: MotorBrain
{
	public        AdvInputAxis  Horizontal;
	public        AdvInputAxis  Vertical;
	private       Coroutine     Coroutine;
	public        FloatVariable CurrentMaxSpeed = 10;
	public        FloatVariable EdgeMaxSpeed    = 10;
	public        FloatVariable MaxSpeed        = 10;
	public        FloatVariable PitchMax        = 0;
	public        FloatVariable YawMax          = 0;
	public        FloatVariable RollMax         = 0;
	private       Vector3       velocity;
	private       Vector3       smoothMoveVec;
	private       float         smoothPitchV;
	private       float         smoothYawV;
	private       float         smoothRollV;
	private       float         pitch;
	private       float         yaw;
	private       float         roll;

	public WorldSettings WorldSettings;

	/// <inheritdoc />
	public override void EnableBrain(Motor motor)
	{
		StopLoop(motor);
		Coroutine = motor.StartCoroutine(BrainLoop(motor));
	}

	/// <inheritdoc />
	public override void DisableBrain(Motor motor)
	{
		StopLoop(motor);
	}

	private void DoMove(Motor motor)
	{
		var smoothDeltaTime = Time.smoothDeltaTime;
		var deltaTime       = Time.deltaTime;
		var targetVelocity  = new Vector3(Horizontal, Vertical).normalized * CurrentMaxSpeed;
		velocity = Vector3.SmoothDamp(velocity, targetVelocity, ref smoothMoveVec, smoothDeltaTime);

		if(velocity.magnitude > CurrentMaxSpeed)
			velocity = velocity.normalized * CurrentMaxSpeed;

		var targetPos = motor.transform.position + ((velocity + (motor.Forward * MaxSpeed)) * deltaTime);

		// constrain pos to cylinder
		var targetPos2D            = new Vector2(targetPos.x, targetPos.y);
		var sqrDstFromCircleCentre = targetPos2D.sqrMagnitude;

		if(sqrDstFromCircleCentre > WorldSettings.SlowDownRadius * WorldSettings.SlowDownRadius)
		{
			var dstFromCentre   = targetPos2D.magnitude;
			var slowDownPercent = 1 - ((WorldSettings.BoundsRadius - dstFromCentre) / WorldSettings.SlowDownBufferRadius);
			CurrentMaxSpeed.Value = Mathf.Lerp(CurrentMaxSpeed, EdgeMaxSpeed, slowDownPercent);
		}
		else
			CurrentMaxSpeed.Value = MaxSpeed;

		if(sqrDstFromCircleCentre + 0.001f > (WorldSettings.BoundsRadius * WorldSettings.BoundsRadius))
		{
			var offset2D = targetPos2D.normalized * WorldSettings.BoundsRadius;
			targetPos = new Vector3(offset2D.x, offset2D.y, targetPos.z);
		}

		motor.SetPosition(targetPos);
	}

	private void DoRotation(Motor motor)
	{
		var smoothDeltaTime = Time.smoothDeltaTime;
		var targetRoll      = -RollMax  * -Horizontal;
		var targetYaw       = YawMax    * -Horizontal;
		var targetPitch     = -PitchMax * -Vertical;
		pitch = Mathf.SmoothDampAngle(pitch, targetPitch, ref smoothPitchV, smoothDeltaTime);
		yaw   = Mathf.SmoothDampAngle(yaw,   targetYaw,   ref smoothYawV,   smoothDeltaTime);
		roll  = Mathf.SmoothDampAngle(roll,  targetRoll,  ref smoothRollV,  smoothDeltaTime);
		motor.SetRotation(new Vector3(pitch, yaw, roll));
	}

	private IEnumerator BrainLoop(Motor motor)
	{
		while(true)
		{
			DoRotation(motor);
			DoMove(motor);

			yield return null;
		}
	}

	private void StopLoop(Motor motor)
	{
		if(Coroutine != null)
			motor.StopCoroutine(Coroutine);
	}

	private void Reset()
	{
		Horizontal = "Horizontal";
		Vertical   = "Vertical";
	}
}
