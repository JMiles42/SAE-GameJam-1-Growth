using UnityEngine;

[CreateAssetMenu(fileName = "Particle Sys Pool", menuName = "Pool/Particle Sys")]
public class ParticleSystemPool: Pool<ParticleSystem>
{
	protected override ParticleSystem Instantiate()
	{
		var partSys = Instantiate(PoolObject, Vector3.zero, Quaternion.identity, Transform);
		partSys.gameObject.SetActive(false);

		return partSys;
	}

	protected override ParticleSystem DoNextPreparation(ParticleSystem partSys)
	{
		if(partSys.gameObject.activeSelf)
		{
			if(OnReposition != null)
				OnReposition.Invoke(partSys);

			partSys.gameObject.SetActive(false);
		}

		return partSys;
	}

	/// <inheritdoc />
	protected override ParticleSystem DoDeletePreparation(ParticleSystem partSys) => partSys;

	/// <inheritdoc />
	protected override bool IsUsable(ParticleSystem partSys) => !partSys.gameObject.activeSelf;
}
