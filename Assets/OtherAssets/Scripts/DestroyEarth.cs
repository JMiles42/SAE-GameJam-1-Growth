using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DestroyEarth : MonoBehaviour {

    public float timer;
    public GameObject earth;
    public GameObject playAgain;
    public GameObject player;

	// Use this for initialization
	void Start () {
        timer = 6f;
	}
	
	// Update is called once per frame
	void Update () {
        timer -= Time.deltaTime;
        if (timer <= 2.5) {
            earth.SetActive(false);
            player.SetActive(false);
                }

        
        if(timer <= 0)
        {
            playAgain.SetActive(true);
        }


	}

    public void PlayAgain()
    {
        SceneManager.LoadScene("GameScene", LoadSceneMode.Single);
    }
}
