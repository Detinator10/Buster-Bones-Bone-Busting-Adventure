using UnityEngine;
using System.Collections;

public class LookScript : MonoBehaviour {
	public PlayerControl player;
	protected ArcherScript body;
	public float lookangle;
	public bool facingLeft;
	public GameObject sprite;
	public float rotateZ = 0.0f;
	// Use this for initialization
	void Start () {
		player = GameObject.Find ("character").GetComponent<PlayerControl>();
		facingLeft = true;
		sprite = transform.parent.gameObject;
	}
	
	// Update is called once per frame
	void Update () {
		var dx = this.transform.position.x - player.transform.position.x;
		var dy = this.transform.position.y - player.transform.position.y;
		var radians = Mathf.Atan2(dy,dx);
		var angle = radians * 180.0f/ Mathf.PI; //I dont't know what is variable lookangle, but maybe you want translate radians to degree
		rotateZ = Mathf.LerpAngle(rotateZ, angle, 5.0f * Time.deltaTime);

		this.transform.rotation = Quaternion.Euler(0, 0, rotateZ);
		//var dir = player.transform.position - transform.position;
		//var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
		//angle -= 180;
		transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
		if (player.transform.position.x > transform.position.x && facingLeft) {
			facingLeft = false;
			var scale = transform.localScale;
			scale.y *= -1;
			transform.localScale = scale;
			scale = sprite.transform.localScale;
			//scale.x *= -1;
			sprite.transform.localScale = scale;
				}
		else if (player.transform.position.x < transform.position.x && !facingLeft){
			facingLeft = true;
			var scale = transform.localScale;
			scale.y = -scale.y;
			this.transform.localScale = scale;
			scale = sprite.transform.localScale;
			//scale.x *= -1;
			sprite.transform.localScale = scale;
		}
}
}
