using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bigger : MonoBehaviour {
    public Dinosaur score;

	// Use this for initialization
	void Start () {
        
          
    }
	
	// Update is called once per frame
	void Update () {

    }

    private void OnCollisionEnter(Collision bigger)
    {


        if (bigger.gameObject.name == "Player")
        {
            
            if (score.boosterOn == false)
            {
                score.score += 1000;
                this.GetComponent<AudioSource>().Play();
                Destroy(this.gameObject);
            }
            else
            {
                score.score += 2000;
                this.GetComponent<AudioSource>().Play();
                Destroy(this.gameObject);
            }
            
        }

    }
}


