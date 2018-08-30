using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TimerUI: MonoBehaviour
{
	public float timer;
	public Text  timerText;

	// Use this for initialization
	private void Start()
	{
		timer = 60f;
	}

	// Update is called once per frame
	private void Update()
	{
		timer          -= Time.deltaTime;
		timerText.text =  "" + (int)timer;

		if(timer <= 0)
		{
			timer = 0;
			SceneManager.LoadScene("LoseScene", LoadSceneMode.Single);
		}
	}
}
