using System.Collections;
using UnityEngine;

public class GUIbestTimeScript : MonoBehaviour {
	public float posX; 
	public float posY; 
	public float sizeX;
	public float sizeY;
	GameWideScript gamescript;

	public string content;
	float dynamicContent;
	public bool BestTime;

	// Use this for initialization
	void Start () {	
		gamescript = (GameWideScript)FindObjectOfType(typeof(GameWideScript));

		if (BestTime == true) {
						dynamicContent = gamescript.levelTimes [gamescript.thislevel];
			dynamicContent = Mathf.Round(dynamicContent * 100f) / 100f;
				} 
		else {
			dynamicContent = gamescript.thisleveltime;
			dynamicContent = Mathf.Round(dynamicContent * 100f) / 100f;
				}

		gamescript.Save ();

		
	}
	
	void OnGUI()
	{

		
		GUI.Label(new Rect (posX, posY, sizeX * Screen.width, sizeY * Screen.height), content + dynamicContent);

		
		
	}
	
	// Update is called once per frame
	void Update () {
		
		
	}
	
}