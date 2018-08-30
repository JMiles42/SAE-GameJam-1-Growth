using UnityEngine;

public class Multiplier: MonoBehaviour
{
	public ActivePowerUp activeicon;
	public GameObject    multiplyIcon;

	// Use this for initialization
	private void Start() { }

	// Update is called once per frame
	private void Update() { }

	private void OnCollisionEnter(Collision collision)
	{
		if(collision.gameObject.name == "Player")
		{
			activeicon.booster.SetActive(true);
			activeicon.boostertimer = 10;
			GetComponent<AudioSource>().Play();
			Destroy(gameObject);
		}
	}
}
