using UnityEngine;

public class Magnet: MonoBehaviour
{
	public ActivePowerUp activeicon;
	public GameObject    magnetIcon;

	// Use this for initialization
	private void Start() { }

	// Update is called once per frame
	private void Update() { }

	private void OnCollisionEnter(Collision collision)
	{
		if(collision.gameObject.name == "Player")
		{
			activeicon.magnet.SetActive(true);
			activeicon.magnettimer = 10;
			GetComponent<AudioSource>().Play();
			Destroy(gameObject);
		}
	}
}
