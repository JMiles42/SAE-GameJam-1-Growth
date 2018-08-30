using UnityEngine;

public class bigger: MonoBehaviour
{
	public Dinosaur score;

	// Use this for initialization
	private void Start() { }

	// Update is called once per frame
	private void Update() { }

	private void OnCollisionEnter(Collision bigger)
	{
		if(bigger.gameObject.name == "Player")
		{
			if(score.boosterOn == false)
			{
				score.score += 1000;
				GetComponent<AudioSource>().Play();
				Destroy(gameObject);
			}
			else
			{
				score.score += 2000;
				GetComponent<AudioSource>().Play();
				Destroy(gameObject);
			}
		}
	}
}
