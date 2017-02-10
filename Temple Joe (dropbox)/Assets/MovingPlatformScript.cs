using UnityEngine;
using System.Collections;

public class MovingPlatformScript : MonoBehaviour {
	public GameObject point1;
	public GameObject point2;
	public bool reverse;
	protected Vector3 x;
	public float speed;
	protected float s;
	public Vector2 point;
	public bool activateWithPlayer;
	bool shouldBeActivated;
	// Use this for initialization
	void Start () {

		if (activateWithPlayer) {

			shouldBeActivated = false;
				}
	if (reverse) {
			point = point1.transform.position;
				}

		else {
			point = point2.transform.position;
				}
		Debug.Log (point1.transform.position.x);
		Debug.Log (point2.transform.position.x);
	}
	
	// Update is called once per frame
	void Update () {
		if (!shouldBeActivated && this.transform.FindChild("character")) {
			shouldBeActivated = true;
				}
		if (shouldBeActivated || !activateWithPlayer) {
						if (this.transform.position.x <= point1.transform.position.x) {
								Debug.Log (this.transform.position.x);
								point = point2.transform.position;
								Debug.Log (speed);
						} else if (this.transform.position.x >= point2.transform.position.x) {
								point = point1.transform.position;
						}
						transform.position = Vector2.MoveTowards (this.transform.position, new Vector2 (point.x, this.transform.position.y), speed * Time.deltaTime);


				}

}
}