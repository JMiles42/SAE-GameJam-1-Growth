using UnityEngine;
using UnityEngine.SceneManagement;

public class DestroyEarth: MonoBehaviour
{
	public GameObject earth;
	public GameObject playAgain;
	public GameObject player;
	public float      timer;

	// Use this for initialization
	private void Start()
	{
		timer = 6f;
	}

	// Update is called once per frame
	private void Update()
	{
		timer -= Time.deltaTime;

		if(timer <= 0.5)
		{
			earth.SetActive(false);
			//player.SetActive(false);
		}

		if(timer <= 0)
			playAgain.SetActive(true);
	}

	public void PlayAgain()
	{
		SceneManager.LoadScene("GameScene", LoadSceneMode.Single);
	}
}
