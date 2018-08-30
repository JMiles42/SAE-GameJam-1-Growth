using UnityEngine;

public class Shield: MonoBehaviour
{
	public ActivePowerUp activeicon;
	public GameObject    shieldIcon;

	// Use this for initialization
	private void Start() { }

	// Update is called once per frame
	private void Update() { }

	private void OnCollisionEnter(Collision collision)
	{
		if(collision.gameObject.name == "Player")
		{
			activeicon.shield.SetActive(true);
			activeicon.shieldtimer = 10;
			Destroy(gameObject);
		}
	}
}
