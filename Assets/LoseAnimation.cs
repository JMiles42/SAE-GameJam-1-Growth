using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseAnimation: MonoBehaviour
{
	public GameObject playAgain;
	public float      timer;
	public float      WaitTime = 0f;

	private void OnCollisionEnter(Collision collision)
	{
		if(collision.gameObject.name == "omnni")
			StartCoroutine(Play());

		print("shatter");
	}

	private IEnumerator Play()
	{
		yield return new WaitForSeconds(WaitTime);

		GetComponent<Animator>().SetTrigger("Play");
	}

	private void Start()
	{
		timer = 5f;
		playAgain.SetActive(false);
	}

	private void Update()
	{
		timer -= Time.deltaTime;

		if(timer <= 0)
			playAgain.SetActive(true);
	}

	public void PlayAgain()
	{
		SceneManager.LoadScene("TestScene", LoadSceneMode.Single);
	}
}
