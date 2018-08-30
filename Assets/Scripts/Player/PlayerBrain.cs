using System.Collections;
using ForestOfChaosLib.AdvVar;
using ForestOfChaosLib.AdvVar.InputSystem;
using ForestOfChaosLib.Attributes;
using UnityEngine;

[CreateAssetMenu(menuName = ButDinoConstants.NAME_ + "Brains/Player Brain")]
public class PlayerBrain: MotorBrain
{
	private const                             float         SMALL_FLOAT = 0.01f;
	private                                   Coroutine     Coroutine;
	public                                    FloatVariable CurrentMaxSpeed = 10;
	public                                    FloatVariable EdgeMaxSpeed    = 10;
	public                                    AdvInputAxis  Horizontal;
	public                                    FloatVariable MaxSpeed = 10;
	[DisableEditing] [SerializeField] private float         pitch;
	public                                    FloatVariable PitchMax = 0;
	[DisableEditing] [SerializeField] private float         roll;
	public                                    FloatVariable RollMax = 0;
	[DisableEditing] [SerializeField] private Vector3       smoothMoveVec;
	[DisableEditing] [SerializeField] private float         smoothPitchV;
	[DisableEditing] [SerializeField] private float         smoothRollV;
	[DisableEditing] [SerializeField] private float         smoothYawV;
	[DisableEditing] [SerializeField] private Vector3       velocity;
	public                                    AdvInputAxis  Vertical;
	public                                    WorldSettings WorldSettings;
	[DisableEditing] [SerializeField] private float         yaw;
	public                                    FloatVariable YawMax = 0;

	private void OnEnable()
	{
		roll          = yaw        = pitch        = 0;
		smoothRollV   = smoothYawV = smoothPitchV = 0;
		smoothMoveVec = velocity   = Vector3.zero;
	}

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

	private IEnumerator BrainLoop(Motor motor)
	{
		while(true)
		{
			if(Horizontal.InputInDeadZone() && Vertical.InputInDeadZone())
			{
				motor.Rigidbody.velocity        = Vector3.zero;
				motor.Rigidbody.angularVelocity = Vector3.zero;
			}

			DoRotation(motor);
			DoMove(motor);

			yield return null;
		}
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

	private void DoMove(Motor motor)
	{
		var smoothDeltaTime = Time.smoothDeltaTime;
		var deltaTime       = Time.deltaTime;
		var targetVelocity  = new Vector3(-Horizontal, -Vertical).normalized * CurrentMaxSpeed;
		velocity = Vector3.SmoothDamp(velocity, targetVelocity, ref smoothMoveVec, smoothDeltaTime);

		if(velocity.magnitude > CurrentMaxSpeed)
			velocity = velocity.normalized * CurrentMaxSpeed;

		var targetPos = motor.transform.position + ((velocity + (motor.Forward * MaxSpeed)) * deltaTime);

		// constrain pos to cylinder
		var targetPos2D            = new Vector2(targetPos.x, targetPos.y);
		var sqrDstFromCircleCentre = targetPos2D.sqrMagnitude;

		if(sqrDstFromCircleCentre + SMALL_FLOAT > WorldSettings.SlowDownRadius * WorldSettings.SlowDownRadius)
		{
			var dstFromCentre   = targetPos2D.magnitude;
			var slowDownPercent = 1 - ((WorldSettings.BoundsRadius - dstFromCentre) / WorldSettings.SlowDownBufferRadius);
			CurrentMaxSpeed.Value = Mathf.Lerp(CurrentMaxSpeed, EdgeMaxSpeed, slowDownPercent);
		}
		else
			CurrentMaxSpeed.Value = MaxSpeed;

		if(sqrDstFromCircleCentre + SMALL_FLOAT > WorldSettings.BoundsRadius * WorldSettings.BoundsRadius)
		{
			var offset2D = targetPos2D.normalized * (WorldSettings.BoundsRadius - SMALL_FLOAT);
			targetPos = new Vector3(offset2D.x, offset2D.y, targetPos.z);
		}

		motor.SetPosition(targetPos);
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
