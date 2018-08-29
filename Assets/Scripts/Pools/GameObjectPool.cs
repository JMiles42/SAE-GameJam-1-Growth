using UnityEngine;

[CreateAssetMenu(fileName = "GO Pool", menuName = "Pool/GO")]
public class GameObjectPool: Pool<GameObject>
{
	protected override GameObject Instantiate()
	{
		var go = Instantiate(PoolObject, Vector3.zero, Quaternion.identity, Transform);
		go.SetActive(false);

		return go;
	}

	protected override GameObject DoNextPreparation(GameObject go)
	{
		if(go.activeSelf)
		{
			if(OnReposition != null)
				OnReposition.Invoke(go);

			go.SetActive(false);
		}

		return go;
	}

	/// <inheritdoc />
	protected override GameObject DoDeletePreparation(GameObject go) => go;

	/// <inheritdoc />
	protected override bool IsUsable(GameObject go) => !go.activeSelf;
}
