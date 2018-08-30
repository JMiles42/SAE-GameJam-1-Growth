using UnityEngine;

public class ActivePowerUp: MonoBehaviour
{
	public GameObject booster;
	public float      boostertimer;
	public GameObject magnet;
	public float      magnettimer;
	public GameObject shield;
	public float      shieldtimer;

	// Use this for initialization
	private void Start() { }

	// Update is called once per frame
	private void Update()
	{
		magnettimer  -= Time.deltaTime;
		shieldtimer  -= Time.deltaTime;
		boostertimer -= Time.deltaTime;

		if(magnettimer < 0)
			magnet.SetActive(false);

		if(shieldtimer < 0)
			shield.SetActive(false);

		if(boostertimer < 0)
			booster.SetActive(false);
	}
}
