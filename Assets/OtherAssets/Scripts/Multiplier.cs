using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Multiplier : MonoBehaviour {

    public GameObject multiplyIcon;
    public ActivePowerUp activeicon;
    

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Player")
        {
            activeicon.booster.SetActive(true);
            activeicon.boostertimer = 10;
            this.GetComponent<AudioSource>().Play();
            Destroy(this.gameObject);
        }

        
    }
}

