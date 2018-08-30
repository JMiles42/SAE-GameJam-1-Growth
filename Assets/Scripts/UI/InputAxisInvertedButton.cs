using ForestOfChaosLib.AdvVar.InputSystem;
using ForestOfChaosLib.FoCsUI.Toggle;
using UnityEngine;

public class InputAxisInvertedButton: MonoBehaviour
{
	public AdvInputAxis         Axis;
	public FoCsToggleClickEvent Toggle;

	private void OnEnable()
	{
		Toggle.onValueChanged -= OnMouseClick;
		Toggle.onValueChanged += OnMouseClick;
	}

	private void OnDisable()
	{
		Toggle.onValueChanged -= OnMouseClick;
	}

	private void Start()
	{
		Toggle.Text    = Axis.Value.Axis;
		Toggle.Toggled = Axis.ValueInverted;
	}

	private void OnMouseClick(bool value)
	{
		Axis.ValueInverted = value;
	}
}
