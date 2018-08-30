using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver: MonoBehaviour
{
	public GameObject reset;

	// Use this for initialization
	private void Start()
	{
		reset.SetActive(false);
	}

	// Update is called once per frame
	private void Update() { }

	public void Retry()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		Time.timeScale = 1;
	}

	//public void Resume()
	//{
	//    Time.timeScale = 1;
	//    Reset.SetActive(false);
	//}

	//public void Exit()
	//{
	//    Application.Quit();
	//}
}
