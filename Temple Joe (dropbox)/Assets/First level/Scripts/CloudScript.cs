using UnityEngine;
using System.Collections;

public class CloudScript : MonoBehaviour {
	public GameObject end;
	public GameObject spawnpos;
	public float speed;
	public Vector3 position;
	public Vector3 originalposition;
	// Use this for initialization
	void Start () {

		originalposition.y = this.transform.position.y;
		originalposition.x = spawnpos.transform.position.x;
		GetComponent<Rigidbody2D>().AddForce(new Vector2(speed,0));
	}

	// Update is called once per frame
	void FixedUpdate () {
		if (this.transform.position.x <= end.transform.position.x) {
			transform.position = originalposition;
		}
	}
}
