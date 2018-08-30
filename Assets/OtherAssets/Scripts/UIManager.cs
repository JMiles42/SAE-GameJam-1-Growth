using UnityEngine;
using UnityEngine.UI;

public class UIManager: MonoBehaviour
{
	public GameOver gameOver;
	public Dinosaur player;
	public Text     scoreText;
	public float    timer;
	public Text     timerText;

	// Use this for initialization
	private void Start()
	{
		timer    = 60f;
		gameOver = GameObject.Find("LoseState").GetComponent<GameOver>();
	}

	// Update is called once per frame
	private void Update()
	{
		timer          -= Time.deltaTime;
		scoreText.text =  "" + player.score;
		timerText.text =  "" + (int)timer;

		if(timer <= 0f)
		{
			gameOver.reset.SetActive(true);
			timer = 0f;
		}
	}
}
