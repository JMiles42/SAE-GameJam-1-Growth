using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MediumShip : MonoBehaviour {
    public Dinosaur score;

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
            
            if (score.boosterOn == false)
            {
                score.score += 750;
                this.GetComponent<AudioSource>().Play();
                Destroy(this.gameObject);
            }
            else
            {
                score.score += 1500;
                this.GetComponent<AudioSource>().Play();
                Destroy(this.gameObject);
            }
        }

    }
}
