using UnityEngine;

public class PowerUpMotorPool: Pool<PowerUpMotor>
{
	public PowerUpMotor Prefab;

	protected override PowerUpMotor Instantiate()
	{
		var go = Instantiate(PoolObject, Vector3.zero, Quaternion.identity, Transform);
		go.gameObject.SetActive(false);

		return go;
	}

	protected override PowerUpMotor DoNextPreparation(PowerUpMotor go)
	{
		if(go.gameObject.activeSelf)
		{
			OnReposition?.Invoke(go);

			go.gameObject.SetActive(false);
		}

		return go;
	}

	/// <inheritdoc />
	protected override PowerUpMotor DoDeletePreparation(PowerUpMotor go) => go;

	/// <inheritdoc />
	protected override bool IsUsable(PowerUpMotor go) => !go.gameObject.activeSelf;
}
