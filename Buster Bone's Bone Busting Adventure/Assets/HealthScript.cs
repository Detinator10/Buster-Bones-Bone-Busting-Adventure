using UnityEngine;
using System.Collections;

public class HealthScript : MonoBehaviour {

    public PlayerControllerScript player;
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Physics2D.OverlapCircle(this.transform.position, .5f) && player.health<2)
        {
            player.health += 1;
            this.gameObject.SetActive(false);
        }
    }
}
