using UnityEngine;

public class LoseDinosaur: MonoBehaviour
{
	private float      delta;
	public  GameObject earth;
	public  float      speed;

	// Use this for initialization
	private void Start()
	{
		speed = 12f;
	}

	// Update is called once per frame
	private void Update()
	{
		delta              = speed * Time.deltaTime;
		transform.position = Vector3.MoveTowards(transform.position, earth.transform.position, delta);
	}

	private void OnCollisionEnter(Collision collision)
	{
		if(collision.gameObject.name == "omnni")
		{
			speed = -1f;
			GetComponent<AudioSource>().Play();
		}
	}
}
