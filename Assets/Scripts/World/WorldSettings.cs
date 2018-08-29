using System.Linq;
using ForestOfChaosLib.Attributes;
using ForestOfChaosLib.Extensions;
using ForestOfChaosLib.Utilities;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(menuName = ButDinoConstants.NAME_ + "World Settings")]
public class WorldSettings: ScriptableObject
{
	[DisableEditing] [SerializeField]                      private float boundsRadius;
	[GetSetter("ConstraintCircleBuffer")] [SerializeField] private float constraintCircleBuffer = 1;
	[GetSetter("GameRadius")] [SerializeField]             private float gameRadius             = 70;
	[GetSetter("SlowDownBufferRadius")] [SerializeField]   private float slowDownBufferRadius   = 10;
	[DisableEditing] [SerializeField]                      private float slowDownRadius;

	public float GameRadius
	{
		get { return gameRadius; }
		set
		{
			gameRadius = value;
			UpdateValues();
		}
	}

	public float ConstraintCircleBuffer
	{
		get { return constraintCircleBuffer; }
		set
		{
			constraintCircleBuffer = value;
			UpdateValues();
		}
	}

	public float SlowDownBufferRadius
	{
		get { return slowDownBufferRadius; }
		set
		{
			slowDownBufferRadius = value;
			UpdateValues();
		}
	}

	public float BoundsRadius
	{
		get { return boundsRadius; }
		private set { boundsRadius = value; }
	}

	public float SlowDownRadius
	{
		get { return slowDownRadius; }
		private set { slowDownRadius = value; }
	}

	public void UpdateValues()
	{
		BoundsRadius   = GameRadius   - ConstraintCircleBuffer;
		SlowDownRadius = BoundsRadius - SlowDownBufferRadius;
	}

	private void OnEnable()
	{
		UpdateValues();
	}
}

internal static class WorldSettingsDraw
{
	[DrawGizmo(GizmoType.Active | GizmoType.Selected | GizmoType.NonSelected, typeof(Motor))]
	private static void DrawGiz(Motor motor, GizmoType giz)
	{
		var wSet = FoCsAssetFinder.FindAssetsByType<WorldSettings>().First();

		if(wSet == null)
			return;

		DrawWorldSettings(wSet, Vector3.zero.SetZ(motor.transform.position));
	}

	private static void DrawWorldSettings(WorldSettings wSet)
	{
		DrawWorldSettings(wSet, Vector3.zero);
	}

	private static void DrawWorldSettings(WorldSettings wSet, Vector3 pos)
	{
		var colourCache = Gizmos.color;
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(pos, wSet.BoundsRadius);
		Gizmos.color = Color.blue;
		Gizmos.DrawWireSphere(pos, wSet.GameRadius);
		Gizmos.color = Color.green;
		Gizmos.DrawWireSphere(pos, wSet.SlowDownBufferRadius);
		Gizmos.color = Color.yellow;
		Gizmos.DrawWireSphere(pos, wSet.SlowDownRadius);
		Gizmos.color = colourCache;
	}
}
