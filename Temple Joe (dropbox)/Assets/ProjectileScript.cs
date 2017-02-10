using UnityEngine;
using System.Collections;

public class ProjectileScript : MonoBehaviour {
	PlayerControl player;
	// Use this for initialization
	void Start () {
		StartCoroutine ("waitForDestroy");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter2D(Collision2D collision){
		Destroy(this.gameObject);
		if (collision.gameObject.name == "character") {
			player = collision.gameObject.GetComponent<PlayerControl>();
			player.Die ();

				}

	}

	IEnumerator waitForDestroy(){
		yield return new WaitForSeconds (2.5f);
		Destroy (this.gameObject);
		}
}
