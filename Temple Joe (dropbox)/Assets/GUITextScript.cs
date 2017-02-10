using UnityEngine;
using System.Collections;

public class GUITextScript : MonoBehaviour {
	public float posX; 
	public float posY; 
	public float sizeX;
	public float sizeY;
	Vector3 thisposition;
	GameWideScript gamescript;
	public PlayerControl player;
	public string content;
	float dynamicContent;
	public string dyncontent;
	public int fontsize;
	// Use this for initialization
	void Start () {	
		player = GameObject.Find ("character").GetComponent<PlayerControl>();


	}
	
	void OnGUI()
	{

			dynamicContent = player.thisleveltime;

		dynamicContent.ToString ("0.0");
		GUI.skin.label.fontSize = fontsize;
		GUI.Label(new Rect (posX * Screen.width, posY * Screen.height, sizeX * Screen.width, sizeY * Screen.height), content + dynamicContent.ToString ("0.00"));

		
	}
	
	// Update is called once per frame
	void Update () {

		
	}
	
}
