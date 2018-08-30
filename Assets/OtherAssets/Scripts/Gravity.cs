
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravity : MonoBehaviour
{

    public Rigidbody rb;
    public GameObject player;
    public ActivePowerUp activePowerup;

    // Use this for initialization
    void Start()
    {
        //player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //if (player.GetComponent<playercontroller>().magnetise == true)
            //{
            //    print("magnet");
            //    StartCoroutine(AttractionCheck());
            //}
            //else
            //{
            //    StopCoroutine(AttractionCheck())
            //}
            player.GetComponent<Dinosaur>().magnetOn = true;
            activePowerup.magnettimer = 10f;
        }

        if (player.GetComponent<Dinosaur>().magnetOn == true)
        {
            float distancebetween = Vector3.Distance(rb.position, player.transform.position);
            if (distancebetween < 5)
            {
                print("magnet");
                StartCoroutine(AttractionCheck());
            }
        }
        else
        {
            StopCoroutine(AttractionCheck());
        }




    }

    IEnumerator AttractionCheck()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            //yield return new WaitForFixedUpdate();


            Vector3 direction = rb.position - player.transform.position;
            float distancebetween = Vector3.Distance(rb.position, player.transform.position);
            if (distancebetween < 4)
            {
                float distance = direction.magnitude;

                float forcemagnitude = (rb.mass * player.GetComponent<Rigidbody>().mass) * 1.5f / Mathf.Pow(distance, 1.5f);
                Vector3 force = direction.normalized * forcemagnitude;
                print("drag");
                rb.AddForce(-force);
            }
            else
            {
                print(distancebetween);
            }

            //yield return new WaitForFixedUpdate();
        }


    }
}
