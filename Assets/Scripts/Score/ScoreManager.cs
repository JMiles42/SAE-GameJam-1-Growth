using System;
using ForestOfChaosLib.AdvVar;
using ForestOfChaosLib.Generics;

public class ScoreManager: Singleton<ScoreManager>
{
	public IntVariable                 Score;
	public static event Func<int, int> OnScoreInterrupt;

	private void Start()
	{
		Score.Value = 0;
	}

	public static void AddScore(int number)
	{
		if(OnScoreInterrupt != null)
			number = OnScoreInterrupt(number);

		Instance.Score.Value += number;
	}
}
