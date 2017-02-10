using UnityEngine;
using System.Collections;

public class ButtonScript : MonoBehaviour {
	public Texture2D icon;
	public float posX; 
	public float posY; 
	public string LevelToLoad;
	public float sizeX;
	public float sizeY;
	public int levelnumber;
	Vector3 thisposition;
	GameWideScript gamescript;

	// Use this for initialization
	void Start () {

		posX = this.transform.position.x;
		posY = this.transform.position.y;
		thisposition = Camera.main.WorldToScreenPoint (new Vector3 (posX, posY, 0));
		thisposition.y = Screen.height - thisposition.y;


	}

	void OnGUI()
	{

						GUI.backgroundColor = Color.red;
						if (GUI.Button (new Rect (thisposition.x, thisposition.y, sizeX * Screen.width, sizeY * Screen.height), icon)) {
								Application.LoadLevel (LevelToLoad);
								
				}
	}
	
	// Update is called once per frame
	void Update () {

	}

}
