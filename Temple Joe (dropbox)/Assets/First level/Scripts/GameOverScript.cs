using UnityEngine;
using System.Collections;


public class GameOverScript : MonoBehaviour {
	public Camera cam;
	public Playertracker CamPos;
	public GameObject GameOverObject;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

			
				
	}
	public void GameOver(){
		this.transform.position = new Vector3(CamPos.Positionx(), CamPos.Positiony(), this.transform.position.z);
	}
}
