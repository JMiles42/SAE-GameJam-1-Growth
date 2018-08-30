using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivePowerUp : MonoBehaviour {
    public GameObject magnet;
    public GameObject shield;
    public GameObject booster;

    public float magnettimer;
    public float shieldtimer;
    public float boostertimer;
	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update ()
    {
        magnettimer -= Time.deltaTime;
        shieldtimer -= Time.deltaTime;
        boostertimer -= Time.deltaTime;

        if (magnettimer < 0)
        {
            magnet.SetActive(false);
        }

        if (shieldtimer < 0)
        {
            shield.SetActive(false);
        }

        if (boostertimer < 0)
        {
            booster.SetActive(false);
        }

    }
}
