using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallShip : MonoBehaviour
{
    public Dinosaur player;

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
            
            if (player.boosterOn == false)
            {
                player.score += 500;
                this.GetComponent<AudioSource>().Play();
                Destroy(this.gameObject);
                
            }
            else
            {
                player.score += 1000;
                this.GetComponent<AudioSource>().Play();
                Destroy(this.gameObject);
            }
            
        }

    }
}
