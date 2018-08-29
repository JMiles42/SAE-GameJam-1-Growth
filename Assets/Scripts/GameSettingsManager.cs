using UnityEngine;

public class GameSettingsManager: MonoBehaviour
{
	public WorldSettings WorldSettings;

	private void OnEnable()
	{
		WorldSettings.GameRadius = 35;
	}
}
