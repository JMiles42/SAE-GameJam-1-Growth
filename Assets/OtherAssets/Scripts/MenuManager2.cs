using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager2: MonoBehaviour
{
	public void BackToMenu()
	{
		SceneManager.LoadScene("Menu", LoadSceneMode.Single);
	}
}
