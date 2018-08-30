using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DinosaurCrash : MonoBehaviour {

    public GameObject earth;
    public float speed;
    float delta;

	// Use this for initialization
	void Start () {
        speed = 12f;
	}
	
	// Update is called once per frame
	void Update () {
        delta = speed * Time.deltaTime;

        this.transform.position = Vector3.MoveTowards(this.transform.position, earth.transform.position, delta);

	}
}
