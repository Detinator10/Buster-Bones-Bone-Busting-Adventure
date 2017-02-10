using UnityEngine;
using System.Collections;

public class SpikeScript : MonoBehaviour {
	public bool startActivated;

	public bool activated;
	public float time;
	public float startTime;
	// Use this for initialization
	IEnumerator Start () {
				if (time == 0) {
						time = 3.0f;
				}


			yield return new WaitForSeconds (startTime);
			if (startActivated) {
								StartCoroutine ("Activate");
						} else {

								StartCoroutine ("Deactivate");
						}
				

				
		}
	
	// Update is called once per frame
	void Update () {
	
	}

	IEnumerator Activate(){
		Debug.Log ("a");
		activated = true;
		GetComponent<Renderer>().enabled = true;
		yield return new WaitForSeconds(time);
		StartCoroutine("Deactivate");
	}

	IEnumerator Deactivate(){
		Debug.Log ("d");
		activated = false;
		GetComponent<Renderer>().enabled = false;
		yield return new WaitForSeconds(time);
		StartCoroutine("Activate");
		}


}

