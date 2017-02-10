using UnityEngine;
using System.Collections;

public class arrowScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter2D(Collision2D coll)
    {
        if(coll.gameObject.tag == "Player")
        {
            coll.gameObject.GetComponent<PlayerControllerScript>().Die(3);
        }
        this.gameObject.SetActive(false);
        Destroy(this.gameObject);
    }
}
