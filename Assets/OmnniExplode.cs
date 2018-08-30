using System.Collections;
using UnityEngine;

public class OmnniExplode: MonoBehaviour
{
	public float WaitTime = 0.5f;

	private void OnCollisionEnter(Collision collision)
	{
		if(collision.gameObject.name == "DinosaurRigger")
			StartCoroutine(Play());

		//print("shatter");
	}

	private IEnumerator Play()
	{
		yield return new WaitForSeconds(WaitTime);

		GetComponent<Animator>().SetTrigger("Play");
		GetComponent<AudioSource>().Play();
	}
}
