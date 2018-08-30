using System.Collections;
using ForestOfChaosLib.Extensions;
using UnityEngine;

public class WorldObjectManager: MonoBehaviour
{
	public MotorRTRef Player;

	private void Update()
	{
		var position = Player.Reference.transform.position;

		for(var i = WorldObject.WorldObjects.Count - 1; i >= 0; i--)
		{
			var worldObject = WorldObject.WorldObjects[i];

			if(position.Distance(worldObject.Position) >= 200)
				worldObject.gameObject.SetActive(false);
		}
	}
}
