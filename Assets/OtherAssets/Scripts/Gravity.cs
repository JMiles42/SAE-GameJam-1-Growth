using System.Collections;
using UnityEngine;

public class Gravity: MonoBehaviour
{
	public ActivePowerUp activePowerup;
	public GameObject    player;
	public Rigidbody     rb;

	// Use this for initialization
	private void Start()
	{
		//player = GameObject.Find("Player");
	}

	// Update is called once per frame
	private void Update()
	{
		if(Input.GetKeyDown(KeyCode.Space))
		{
			//if (player.GetComponent<playercontroller>().magnetise == true)
			//{
			//    print("magnet");
			//    StartCoroutine(AttractionCheck());
			//}
			//else
			//{
			//    StopCoroutine(AttractionCheck())
			//}
			player.GetComponent<Dinosaur>().magnetOn = true;
			activePowerup.magnettimer                = 10f;
		}

		if(player.GetComponent<Dinosaur>().magnetOn)
		{
			var distancebetween = Vector3.Distance(rb.position, player.transform.position);

			if(distancebetween < 5)
			{
				print("magnet");
				StartCoroutine(AttractionCheck());
			}
		}
		else
			StopCoroutine(AttractionCheck());
	}

	private IEnumerator AttractionCheck()
	{
		while(true)
		{
			yield return new WaitForSeconds(1f);
			//yield return new WaitForFixedUpdate();

			var direction       = rb.position - player.transform.position;
			var distancebetween = Vector3.Distance(rb.position, player.transform.position);

			if(distancebetween < 4)
			{
				var distance       = direction.magnitude;
				var forcemagnitude = (rb.mass * player.GetComponent<Rigidbody>().mass * 1.5f) / Mathf.Pow(distance, 1.5f);
				var force          = direction.normalized                                     * forcemagnitude;
				print("drag");
				rb.AddForce(-force);
			}
			else
				print(distancebetween);

			//yield return new WaitForFixedUpdate();
		}
	}
}
