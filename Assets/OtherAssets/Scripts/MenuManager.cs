using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager: MonoBehaviour
{
	public void PlayGame()
	{
		SceneManager.LoadScene("TestScene", LoadSceneMode.Single);
	}

	public void Credits()
	{
		SceneManager.LoadScene("Credits", LoadSceneMode.Single);
	}
}
