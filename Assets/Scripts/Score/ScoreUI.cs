using ForestOfChaosLib.AdvVar;
using ForestOfChaosLib.FoCsUI.Image;
using UnityEngine;

public class ScoreUI: MonoBehaviour
{
	[SerializeField] private FoCsTextEvent textEvent;

	public FoCsTextEvent TextEvent
	{
		get { return textEvent ?? (textEvent = GetComponent<FoCsTextEvent>()); }
		set { textEvent = value; }
	}

	public IntReference Score;

	private void OnEnable()
	{
		Score.OnValueChange += OnValueChange;
	}

	private void OnDisable()
	{
		Score.OnValueChange -= OnValueChange;
	}

	private void Start()
	{
		DrawScore(0);
	}

	private void OnValueChange()
	{
		DrawScore(Score.Value);
	}

	private void DrawScore(int number)
	{
		TextEvent.Text = $"Score: {number}";
	}
}
