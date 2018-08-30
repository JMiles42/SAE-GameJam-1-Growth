using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smaller : MonoBehaviour {

    public Dinosaur score;
    
    // Use this for initialization
    void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter(Collision small)
    {
        

        if (small.gameObject.name == "Player")
        {
            
            if (score.shieldOn == false)
            {
                score.score -= 500;
                
                Destroy(this.gameObject);
            }
            else
            {
                
                Destroy(this.gameObject);
            }
            
        }


    }
}
