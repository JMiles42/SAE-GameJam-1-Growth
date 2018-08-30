using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager2: MonoBehaviour
{
	// Use this for initialization
	private void Start() { }

	// Update is called once per frame
	private void Update() { }

	public void BackToMenu()
	{
		SceneManager.LoadScene("Menu", LoadSceneMode.Single);
	}
}
