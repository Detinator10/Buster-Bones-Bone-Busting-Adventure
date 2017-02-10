using UnityEngine;
using System.Collections;

public class PauseMenu : MonoBehaviour {
	public GameWideScript gamescript;
	bool paused;
	public float resumex;
	public float resumey;
	public float resumesizex;
	public float resumesizey;
	public Texture2D resumeicon;
	public float restartx;
	public float restarty;
	public float restartsizex;
	public float restartsizey;
	public Texture2D restarticon;
	public float menux;
	public float menuy;
	public float menusizex;
	public float menusizey;
	public Texture2D menuicon;

	// Use this for initialization
	void Start () {
		gamescript =  (GameWideScript)FindObjectOfType(typeof(GameWideScript));
	}
	
	// Update is called once per frame
	void Update () {
	if(Input.GetKeyDown(KeyCode.P)){
			gamescript.Pause();
			paused = true;
			}
		}


	void OnGUI(){
		if (paused) {
			if(GUI.Button (new Rect(resumex *Screen.width, resumey * Screen.height, resumesizex * Screen.width, resumesizey * Screen.height), resumeicon)){
				gamescript.Resume();
				paused = false;
			}
			if(GUI.Button (new Rect(restartx *Screen.width, restarty * Screen.height, restartsizex * Screen.width, restartsizey * Screen.height), restarticon)){
				gamescript.Resume();
				gamescript.Restart();
			}
			if(GUI.Button (new Rect(menux *Screen.width, menuy * Screen.height, menusizex * Screen.width, menusizey * Screen.height), menuicon)){
				gamescript.Resume();
				Application.LoadLevel("Title Screen");
			}

		}
}
	}
