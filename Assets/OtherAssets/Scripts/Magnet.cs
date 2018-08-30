using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Magnet : MonoBehaviour {

    public GameObject magnetIcon;
    public ActivePowerUp activeicon;
    
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Player")
        {
            activeicon.magnet.SetActive(true);
            activeicon.magnettimer = 10;
            this.GetComponent<AudioSource>().Play();
            Destroy(this.gameObject);
        }
    }
}
