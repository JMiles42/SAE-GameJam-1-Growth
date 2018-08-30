using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shield : MonoBehaviour {

    public GameObject shieldIcon;
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
            activeicon.shield.SetActive(true);
            activeicon.shieldtimer = 10;
            
            Destroy(this.gameObject);
        }
    }
}

