using UnityEngine;

public class GameSettingsManager : MonoBehaviour
{
	public WorldSettings WorldSettings;

	void OnEnable()
	{
		WorldSettings.GameRadius = 40;
	}
}
