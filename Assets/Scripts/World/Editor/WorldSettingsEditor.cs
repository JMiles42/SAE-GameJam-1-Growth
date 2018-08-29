using ForestOfChaosLib.Editor;
using UnityEditor;

[CustomEditor(typeof(WorldSettings))]
public class WorldSettingsEditor: FoCsEditor<WorldSettings>
{
	protected override void DoExtraDraw()
	{
		if(FoCsGUI.Layout.Button("Update"))
		{
			Target.UpdateValues();
			serializedObject.Update();
			serializedObject.ApplyModifiedProperties();
		}
	}
}
