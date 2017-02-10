using UnityEngine;
using System.Collections;

public class endpoint : MonoBehaviour {

    public string level;
    public int currentLevel;
    public GameManager GM;
	// Use this for initialization
	void Start () {
        GM = GameObject.Find("_GM").GetComponent<GameManager>();
        GameManager.currentLevel = currentLevel;
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
