using UnityEngine;
using System.Collections;

public class LevelButtonScript : MonoBehaviour {

	public Texture2D icon;
	public float posX; 
	public float posY; 
	public string LevelToLoad;
	public float sizeX;
	public float sizeY;
	public int levelnumber;
	Vector3 thisposition;
	GameWideScript gamescript;
	FadeScript fade;
	
	// Use this for initialization
	void Start () {
		
		posX = this.transform.position.x;
		posY = this.transform.position.y;
		thisposition = Camera.main.WorldToScreenPoint (new Vector3 (posX, posY, 0));
		thisposition.y = Screen.height - thisposition.y;
		gamescript = (GameWideScript)FindObjectOfType(typeof(GameWideScript));
		fade = (FadeScript)FindObjectOfType (typeof(FadeScript));

	}
	
	void OnGUI()
	{
		if (!fade.fadingOut) {
						if (gamescript.ReachedLevel >= levelnumber) {
								GUI.backgroundColor = Color.red;
								if (GUI.Button (new Rect (thisposition.x, thisposition.y, sizeX * Screen.width, sizeY * Screen.height), icon)) { 
										gamescript.thislevel = levelnumber;
										Debug.Log (gamescript.thislevel);
										fade.FadeOut ();
										Debug.Log ("wait");
										StartCoroutine ("Wait");

								}
						}
				}
	}
	
	// Update is called once per frame
	void Update () {

	}
	
	public IEnumerator Wait(){
		Debug.Log ("waiting");
		yield return new WaitForSeconds (2.1f);
		Application.LoadLevel (LevelToLoad);
	}
}
