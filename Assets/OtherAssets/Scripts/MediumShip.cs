using UnityEngine;

public class MediumShip: MonoBehaviour
{
	public Dinosaur score;

	// Use this for initialization
	private void Start() { }

	// Update is called once per frame
	private void Update() { }

	private void OnCollisionEnter(Collision collision)
	{
		if(collision.gameObject.name == "Player")
		{
			if(score.boosterOn == false)
			{
				score.score += 750;
				GetComponent<AudioSource>().Play();
				Destroy(gameObject);
			}
			else
			{
				score.score += 1500;
				GetComponent<AudioSource>().Play();
				Destroy(gameObject);
			}
		}
	}
}
