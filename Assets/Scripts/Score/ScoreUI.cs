using ForestOfChaosLib.AdvVar;
using ForestOfChaosLib.FoCsUI.Image;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreUI: MonoBehaviour
{
	public                   IntReference  Score;
	[SerializeField] private FoCsTextEvent textEvent;

	public FoCsTextEvent TextEvent
	{
		get { return textEvent ?? (textEvent = GetComponent<FoCsTextEvent>()); }
		set { textEvent = value; }
	}

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

	private void Update()
	{
		if(Score.Value >= 1500)
			SceneManager.LoadScene("WinningScene", LoadSceneMode.Single);
	}
}
