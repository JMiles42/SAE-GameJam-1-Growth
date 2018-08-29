using ForestOfChaosLib.Extensions;
using ForestOfChaosLib.Generics;

public class PoolManager: Singleton<PoolManager>
{
	public Pool[] Pools;

	private void Start()
	{
		foreach(var pool in Pools)
			pool.InitPool();
	}

	public void DisableAllChildren()
	{
		foreach(var pool in Pools)
			pool.Transform.SetActiveChildren(false);
	}
}
