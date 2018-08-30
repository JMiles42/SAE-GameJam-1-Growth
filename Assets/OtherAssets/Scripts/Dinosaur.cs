using UnityEngine;

public class Dinosaur: MonoBehaviour
{
	public  GameObject badAudio;
	public  bool       boosterOn;
	public  GameOver   gameOver;
	public  GameObject goodAudio;
	public  GameObject magnet;
	public  bool       magnetOn;
	public  GameObject magnetParticles;
	public  GameObject multiplier;
	public  Color      originalColor;
	private float      pickupScore;
	public  float      score;
	public  GameObject shield;

	// public ActivePowerUp activePowerup;
	public bool shieldOn;

	// Use this for initialization
	private void Start()
	{
		score         = 0;
		gameOver      = GameObject.Find("LoseState").GetComponent<GameOver>();
		pickupScore   = 100;
		boosterOn     = false;
		shieldOn      = false;
		magnetOn      = false;
		originalColor = GetComponent<Renderer>().material.color;
	}

	// Update is called once per frame
	private void Update()
	{
		// if (activePowerup.boostertimer > 0)
		// {
		//     boosterOn = true;
		// }
		//else
		// {
		//     boosterOn = false;
		// }

		//if(activePowerup.shieldtimer > 0)
		//{
		//    shieldOn = true;
		//}
		//else
		//{
		//    shieldOn = false;
		//}

		//if(activePowerup.magnettimer > 0)
		//{
		//    magnetOn = true;
		//}
		//else
		//{
		//    magnetOn = false;
		//}

		if(score <= -500f)
			gameOver.reset.SetActive(true);

		if(shieldOn)
			GetComponent<Renderer>().material.color = Color.green;
		else
			GetComponent<Renderer>().material.color = originalColor;

		if(magnetOn)
		{
			magnetParticles.SetActive(true);
			GetComponent<Renderer>().material.color = Color.white;
		}
		else
		{
			// magnetParticles.SetActive(false);
			GetComponent<Renderer>().material.color = originalColor;
		}

		if(boosterOn)
			GetComponent<Renderer>().material.color = Color.yellow;
		else
			GetComponent<Renderer>().material.color = originalColor;
	}

	private void OnCollisionEnter(Collision collision)
	{
		if(collision.gameObject.tag == "Pickup")
		{
			//activePowerup.GetComponent<AudioSource>().Play();
			if(boosterOn == false)
				score += pickupScore;
			else
				score += pickupScore * 2;
		}

		if(collision.gameObject.tag == "BadThing")
		{
			badAudio.GetComponent<AudioSource>().Play();

			if(shieldOn == false)
				transform.localScale = new Vector3(transform.localScale.x * 0.8f, transform.localScale.y * 0.8f, transform.localScale.z * 0.8f);
		}

		if(collision.gameObject.tag == "GoodThing")
		{
			goodAudio.GetComponent<AudioSource>().Play();
			transform.localScale = new Vector3(transform.localScale.x * 1.2f, transform.localScale.y * 1.2f, transform.localScale.z * 1.2f);
		}
	}
}
