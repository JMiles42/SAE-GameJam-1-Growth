using UnityEngine;

[CreateAssetMenu(menuName = "Pool/World Object")]
public class WorldObjectPool: Pool<WorldObject>
{
	protected override WorldObject Instantiate()
	{
		var go = Instantiate(PoolObject, Vector3.zero, Quaternion.identity, Transform);
		go.gameObject.SetActive(false);

		return go;
	}

	protected override WorldObject DoNextPreparation(WorldObject go)
	{
		if(go.gameObject.activeSelf)
		{
			OnReposition?.Invoke(go);
			go.gameObject.SetActive(false);
		}

		return go;
	}

	/// <inheritdoc />
	protected override WorldObject DoDeletePreparation(WorldObject go) => go;

	/// <inheritdoc />
	protected override bool IsUsable(WorldObject go) => !go.gameObject.activeSelf;
}
