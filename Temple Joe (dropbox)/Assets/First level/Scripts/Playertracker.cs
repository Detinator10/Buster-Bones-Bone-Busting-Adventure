using UnityEngine;
using System.Collections;


public class Playertracker : MonoBehaviour {
	public float xMargin = 1f;
	public float yMargin = 1f;
	public float xSmooth = 8f;
	public float ySmooth = 8f;
	public Vector2 maxXandY;
	public Vector2 minXandY;
	public float targetX;
	public float targetY;
	float camx;
	float camy;
	float midX;
	float midY;
	public Transform DeathHeight;

	private Transform Player;

	void Start(){
		Screen.SetResolution (1366, 768, true);
		midX = this.GetComponent<Camera>().pixelWidth / 2;
		midY = this.GetComponent<Camera>().pixelHeight / 2;
	}
	void Awake ()
	{
		Player = GameObject.FindGameObjectWithTag ("Player").transform;
	}

	bool CheckYMargin()
	{
		return Mathf.Abs (transform.position.y - Player.position.y) > yMargin;
	}

	bool CheckXMargin()
	{
		return Mathf.Abs (transform.position.x - Player.position.x) > xMargin;
	}

	void FixedUpdate ()
	{
		if(Player.transform.position.y > DeathHeight.position.y){
		TrackPlayer ();
		}
	}

	void TrackPlayer()
	{
		targetX = transform.position.x;
		targetY = transform.position.y;

		if (CheckXMargin ())
			targetX = Mathf.Lerp (transform.position.x, Player.position.x, xSmooth * Time.deltaTime);

		if (CheckYMargin ())
			targetY = Mathf.Lerp (transform.position.y, Player.position.y, ySmooth * Time.deltaTime);

		targetY = Mathf.Clamp (targetY, minXandY.y, maxXandY.y);

		transform.position = new Vector3 (targetX, targetY, transform.position.z);
	}

	public float Positionx(){
		camx = this.transform.position.x + midX;

		return camx;
		}
	public float Positiony(){
		camy = this.transform.position.y + midY;

		return camy;
		}

}
