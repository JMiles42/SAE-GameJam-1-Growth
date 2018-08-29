using System;
using ForestOfChaosLib.AdvVar;
using ForestOfChaosLib.Generics;

public class ScoreManager: Singleton<ScoreManager>
{
	public IntVariable                 Score;
	public static event Func<int, int> OnScoreInterupt;

	public static void AddScore(int number)
	{
		if(OnScoreInterupt != null)
			number = OnScoreInterupt(number);

		Instance.Score.Value = number;
	}
}
