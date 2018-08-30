using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOver : MonoBehaviour {

    public GameObject reset;


    // Use this for initialization
    void Start()
    {
        reset.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;

    }

    //public void Resume()
    //{
    //    Time.timeScale = 1;
    //    Reset.SetActive(false);
    //}

    //public void Exit()
    //{
    //    Application.Quit();
    //}

}
