using UnityEngine;
using System.Collections;

public class CoinScript : MonoBehaviour {

    public PlayerControllerScript player;
    public LayerMask whatIsPlayer;
    public GameManager GM;
	// Use this for initialization
	void Start () {
        GM = GameObject.Find("_GM").GetComponent<GameManager>();
    }
	
	// Update is called once per frame
	void Update () {
        if(Physics2D.OverlapCircle(this.transform.position, .2f, whatIsPlayer))
        {
            player.coinScore += 1;
            GM.score[GameManager.player] += 200;
            this.gameObject.SetActive(false);
        }
	}
}
