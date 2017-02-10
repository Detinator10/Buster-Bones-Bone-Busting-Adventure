using UnityEngine;
using System.Collections;

public class DeleteScript : MonoBehaviour {

	public GameWideScript gamescript;
	// Use this for initialization
	void Start () {
		gamescript =  (GameWideScript)FindObjectOfType(typeof(GameWideScript));
	}
	
	// Update is called once per frame
	void Update () {
	if (Input.GetKeyDown (KeyCode.Escape)) {
			gamescript.Delete ();
				}
	}
}
