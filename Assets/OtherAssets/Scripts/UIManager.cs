using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    public Dinosaur player;
    public Text scoreText;
    public Text timerText;
    public float timer;

    public GameOver gameOver;

    // Use this for initialization
    void Start () {
        timer = 60f;
        gameOver = GameObject.Find("LoseState").GetComponent<GameOver>();
    }
	
	// Update is called once per frame
	void Update () {
        timer -= Time.deltaTime;
        

        scoreText.text = ("" + player.score);
        timerText.text = (""+(int)timer);


        if (timer <= 0f)
        {
            gameOver.reset.SetActive(true);
            timer = 0f;
        }
    }
}
