using UnityEngine;    // For Debug.Log, etc.
using System.Collections;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

using System;
using System.Runtime.Serialization;
using System.Reflection;

public class PlayerControl : MonoBehaviour {
	public float maxSpeed = 5f;
	bool facingRight = true;
	public Animator anim;
	bool grounded = false;
	public Transform groundCheck;
	float groundRadius = 0.2f;
	public LayerMask whatIsGround;
	public float jumpForce = 1000f;
	public GameObject DeathHeight;
	public bool Dead = false;
	public GameWideScript gamescript;
	public GameObject levelend;
	public GameObject EnemyCheck;
	public LayerMask whatisenemy;
	public Collider2D EnemyInRange;
	EnemyScript Enemyscript;
	public float move;
	public GameObject EnemyToBeKilled;
	public float thisleveltime;
	public bool winning;
	bool spiked;
	public LayerMask whatIsSpike;
	public LayerMask whatIsMovingPlatform;
	public bool onMovingPlatform;
	public Collider2D spike;
	SpikeScript spikescript;
	public Collider2D movingplat;
	EnemyChargerScript enemyscript2;
	public bool ONLYSETDURINGPLAY;


		
		
		
	// Use this for initialization
	void Start () {
		DeathHeight = GameObject.Find ("Death Elevation");
		levelend = GameObject.Find ("Level End");
		gamescript =  (GameWideScript)FindObjectOfType(typeof(GameWideScript));
		Debug.Log (gamescript.ReachedLevel);
		Debug.Log (gamescript.levelTimes [gamescript.thislevel]);
		anim = GetComponent<Animator> ();
		thisleveltime = 0;



		
	}
	void Update()
	{
		if (!Dead && !winning) {
						thisleveltime += Time.deltaTime;
				}
	}
	// Update is called once per frame
	void FixedUpdate () {
		Dead = anim.GetBool ("Dead");
		if (!anim.GetBool ("Dead") && this.transform.position.y != DeathHeight.transform.position.y) {
			if(!grounded && GetComponent<Rigidbody2D>().velocity.y == 0){
				GetComponent<Rigidbody2D>().velocity = new Vector2(0,-20);
			}
			onMovingPlatform = Physics2D.OverlapCircle (groundCheck.position, groundRadius, whatIsMovingPlatform);
			grounded = Physics2D.OverlapCircle (groundCheck.position, groundRadius, whatIsGround);
			spiked = Physics2D.OverlapCircle (groundCheck.position, groundRadius, whatIsSpike);

			if(onMovingPlatform){
				movingplat = Physics2D.OverlapCircle (groundCheck.position, groundRadius, whatIsMovingPlatform);
				this.transform.parent = movingplat.GetComponent<Transform>();
			}
			else if (!onMovingPlatform){
				this.transform.parent = null;
			}
			grounded = Physics2D.OverlapCircle (groundCheck.position, groundRadius, whatIsGround);
			if(spiked){
				spike = Physics2D.OverlapCircle (groundCheck.position, groundRadius, whatIsSpike);
				spikescript = spike.GetComponent<SpikeScript>();
				if(spikescript.activated){
					Die ();
				}


			}

			anim.SetBool ("Ground", grounded);
			anim.SetFloat ("vSpeed", GetComponent<Rigidbody2D>().velocity.y);
			grounded = Physics2D.OverlapCircle (groundCheck.position, groundRadius, whatIsGround);
			move = Input.GetAxis ("Horizontal");
			anim.SetFloat ("Speed", Mathf.Abs (move));

			if(Input.GetKeyDown (KeyCode.LeftAlt)){
				Attack();
			}

			GetComponent<Rigidbody2D>().velocity = new Vector2 (move * maxSpeed, GetComponent<Rigidbody2D>().velocity.y);
			if (move > 0 && !facingRight)
				Flip ();
			
			if (move < 0 && facingRight)
				Flip ();

			if(this.transform.position.x > levelend.transform.position.x &&  this.transform.position.y <= DeathHeight.transform.position.y)
			{
				StartCoroutine( "Win");
			}
			
			else if (this.transform.position.y <= DeathHeight.transform.position.y){
				this.GetComponent<Rigidbody2D>().velocity = new Vector2(0,0);
				Die ();
			}
			grounded = Physics2D.OverlapCircle (groundCheck.position, groundRadius, whatIsGround);
			if (grounded && Input.GetKeyDown (KeyCode.Space)) {
				anim.SetBool ("Ground", false);
				GetComponent<Rigidbody2D>().AddForce (new Vector2 (0, jumpForce));
				
				
			}

		}

		
	}
	void Flip()
	{
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
	
	public void Die()
	{
		if (!ONLYSETDURINGPLAY) {
						Debug.Log ("Pdie");
						anim.SetBool ("Dead", true);
						gamescript.GameOver ();
				}
	}

	IEnumerator Win(){
		winning = true;
		gamescript.thisleveltime = thisleveltime;
		if (gamescript.levelTimes [gamescript.thislevel] > gamescript.thisleveltime || gamescript.levelTimes[gamescript.thislevel] == 0) {
			gamescript.levelTimes [gamescript.thislevel] = gamescript.thisleveltime;
				}

		if (Application.loadedLevelName == "Level " + gamescript.ReachedLevel) {
			gamescript.ReachedLevel +=1;
		}



		yield return new WaitForSeconds (1f);
		Debug.Log ("!!!!!!!!! " + gamescript.ReachedLevel + " !!!!!!!");
		foreach(float h in gamescript.levelTimes)
		{
			Debug.Log (h);
		}
		gamescript.Save ();
		Application.LoadLevel ("Win Screen");
		}

	private void Attack(){
		Debug.Log ("PlayerAttack");
		if (!anim.GetBool ("Attacking")) {
						EnemyInRange = Physics2D.OverlapCircle (EnemyCheck.transform.position, 1.0f, whatisenemy);
						if (EnemyInRange) {
								anim.SetBool ("Attacking", true);
								EnemyToBeKilled = EnemyInRange.gameObject;

								Enemyscript = EnemyToBeKilled.GetComponent <EnemyScript> ();
								StartCoroutine (Kill ());
			
						}
				}
		}

	IEnumerator Kill(){
		yield return new WaitForSeconds (0.2f);
		Enemyscript.Die ();
		yield return new WaitForSeconds (1.5f);
		anim.SetBool("Attacking", false);
		Enemyscript = null;
	}

	protected void OnTriggerEnter2D(Collider2D collider){
		Debug.Log ("collision");
		if (collider.gameObject.layer == 10)
						Die ();

		}

	
}