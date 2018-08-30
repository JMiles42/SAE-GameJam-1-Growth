using UnityEngine;

public class Smaller: MonoBehaviour
{
	public Dinosaur score;

	// Use this for initialization
	private void Start() { }

	// Update is called once per frame
	private void Update() { }

	private void OnCollisionEnter(Collision small)
	{
		if(small.gameObject.name == "Player")
		{
			if(score.shieldOn == false)
			{
				score.score -= 500;
				Destroy(gameObject);
			}
			else
				Destroy(gameObject);
		}
	}
}
