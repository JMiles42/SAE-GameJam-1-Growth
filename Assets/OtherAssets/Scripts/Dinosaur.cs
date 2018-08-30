using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Dinosaur : MonoBehaviour {

    public float score;
    
    public GameObject shield;
    public GameObject multiplier;
    public GameObject magnet;
    float pickupScore;
    public bool boosterOn;
    public ActivePowerUp activePowerup;
    public bool shieldOn;
    public Color originalColor;
    public GameOver gameOver;
    public GameObject magnetParticles;
    public bool magnetOn;
    public GameObject badAudio;
    public GameObject goodAudio;
    
    
    

    // Use this for initialization
    void Start () {
        
        score = 0;
        gameOver = GameObject.Find("LoseState").GetComponent<GameOver>();
        pickupScore = 100;
        boosterOn = false;
        shieldOn = false;
        magnetOn = false;
        originalColor = this.GetComponent<Renderer>().material.color;
        
       
    }
	
	// Update is called once per frame
	void Update () {
        

        if (activePowerup.boostertimer > 0)
        {
            boosterOn = true;
        }
        else
        {
            boosterOn = false;
        }

        if(activePowerup.shieldtimer > 0)
        {
            shieldOn = true;
        }
        else
        {
            shieldOn = false;
        }

        if(activePowerup.magnettimer > 0)
        {
            magnetOn = true;
        }
        else
        {
            magnetOn = false;
        }

        if (score <= -500f)
        {
            gameOver.reset.SetActive(true);
        }

        if (shieldOn)
        {
            this.GetComponent<Renderer>().material.color = Color.green;
        }
        else
        {
            this.GetComponent<Renderer>().material.color = originalColor;
        }

        if (magnetOn)
        {
            magnetParticles.SetActive(true);
            this.GetComponent<Renderer>().material.color = Color.white;
           
        }
        else
        {
            magnetParticles.SetActive(false);
            this.GetComponent<Renderer>().material.color = originalColor;
        }

        if (boosterOn)
        {
            this.GetComponent<Renderer>().material.color = Color.yellow;
        }
        else
        {
            this.GetComponent<Renderer>().material.color = originalColor;
        }
        
       
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Pickup")
        {
            activePowerup.GetComponent<AudioSource>().Play();
            if (boosterOn == false)
            {
                score += pickupScore;
            }
            else
            {
                score += pickupScore * 2;

            }
        }

        if (collision.gameObject.tag == "BadThing")
        {
            badAudio.GetComponent<AudioSource>().Play();
            if (shieldOn == false)
            {
                this.transform.localScale = new Vector3(this.transform.localScale.x * 0.8f, this.transform.localScale.y *0.8f, this.transform.localScale.z *0.8f);
            }
            
        }

        if (collision.gameObject.tag == "GoodThing")
        {
            goodAudio.GetComponent<AudioSource>().Play();
            this.transform.localScale = new Vector3(this.transform.localScale.x * 1.2f, this.transform.localScale.y *1.2f, this.transform.localScale.z *1.2f);
        }

        }


    
}
