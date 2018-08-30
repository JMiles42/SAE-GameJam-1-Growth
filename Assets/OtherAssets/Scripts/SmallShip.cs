using UnityEngine;

public class SmallShip: MonoBehaviour
{
	public Dinosaur player;

	// Use this for initialization
	private void Start() { }

	// Update is called once per frame
	private void Update() { }

	private void OnCollisionEnter(Collision collision)
	{
		if(collision.gameObject.name == "Player")
		{
			if(player.boosterOn == false)
			{
				player.score += 500;
				GetComponent<AudioSource>().Play();
				Destroy(gameObject);
			}
			else
			{
				player.score += 1000;
				GetComponent<AudioSource>().Play();
				Destroy(gameObject);
			}
		}
	}
}
