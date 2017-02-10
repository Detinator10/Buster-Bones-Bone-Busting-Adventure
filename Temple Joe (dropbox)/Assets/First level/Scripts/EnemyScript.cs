using UnityEngine;
using System.Collections;

public class EnemyScript : MonoBehaviour {
	protected bool facingRight = true;
	public LayerMask whatIsGround;
	public LayerMask whatIsPlayer;
	protected float groundRadius = 0.2f;
	protected float attackRadius = 0.2f;
	public Transform GroundCheck;
	public Transform PlayerCheck2;
	public bool grounded = false;
	public bool Dead = false;
	public Transform onEdgeCheck;
	public bool NotonEdge;
	public Transform PlayerCheck;
	public float originalmove;
	protected float newmove;
	protected Vector3 BackupPosition;
	protected Animator anim;
	public GameObject Player;
	public bool dead;
	public PlayerControl playerscript;
	public GameWideScript gamescript;
	public bool activated = false;
	protected Vector3 inview;
	protected bool pushed = false;
	protected bool onMovingPlatform;
	public LayerMask whatIsMovingPlatform;
	protected Collider2D movingplat;
	public bool exception;
	protected int lives;
	protected Vector3 theScale;
	
	
	
	
	
	
	
	// Use this for initialization
	protected virtual void Start () {
		Player = GameObject.Find ("character");
		playerscript = Player.GetComponent<PlayerControl> ();
		anim = GetComponent<Animator> ();
		anim.SetBool ("Attacking",false);
		whatIsMovingPlatform = LayerMask.GetMask ("MovingPlatforms");
		lives = 1;
		gamescript =  (GameWideScript)FindObjectOfType(typeof(GameWideScript));
		
	}
	
	// Update is called once per frame
	protected virtual void FixedUpdate () {
		onMovingPlatform = Physics2D.OverlapCircle (GroundCheck.position, groundRadius, whatIsMovingPlatform);


		if(onMovingPlatform){
			movingplat = Physics2D.OverlapCircle (GroundCheck.position, groundRadius, whatIsMovingPlatform);
			this.transform.parent = movingplat.GetComponent<Transform>();
		}
		if (!activated) {
			StartCoroutine(Activate());
		}
		if (playerscript.Dead) {
			anim.SetBool("Attacking", false);
				}
		
		
		if(!anim.GetBool("Dead") && playerscript.Dead == false && !anim.GetBool("Attacking")  && activated && grounded && !exception)
		{
			
			
			
			
			
			if (this.transform.position.x < Player.transform.position.x){
				newmove = originalmove;
				
			}
			if (this.transform.position.x > Player.transform.position.x){
				newmove = originalmove * -1;
				
			}
			
			GetComponent<Rigidbody2D>().velocity = new Vector2 (newmove, GetComponent<Rigidbody2D>().velocity.y);
			
			anim.SetFloat("Speed",Mathf.Abs(newmove));
			if (newmove < 0 && !facingRight)
				Flip ();
			
			if (newmove > 0 && facingRight)
				Flip ();
			
			
			
			
			
			
			
			
			
			BackupPosition = this.transform.position;
			
			
			if(Physics2D.OverlapCircle (PlayerCheck.position, attackRadius, whatIsPlayer)){
				Attack ();
			}

			MainLoopCode();

		}

		grounded = Physics2D.OverlapCircle (GroundCheck.position, groundRadius, whatIsGround);
		if (!grounded){
			ifnotgrounded();
		}
		if (playerscript.Dead == true) {
			this.GetComponent<Rigidbody2D>().velocity = new Vector2 (0, this.GetComponent<Rigidbody2D>().velocity.y);
			anim.SetBool("Pdead", true);
		}

		
	}



	protected virtual void ifnotgrounded(){
		this.GetComponent<Rigidbody2D>().velocity = new Vector2(0,0);
		this.transform.position = BackupPosition;
	}
	protected virtual void Flip()
	{
		facingRight = !facingRight;
		theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
	
	protected virtual void Attack()
	{
		if(!anim.GetBool("Dead")){
			anim.SetBool("Attacking", true);
		}
		GetComponent<Rigidbody2D>().velocity = new Vector2 (0, 0);
		StartCoroutine(attack ());
		
		
	}
	public void Die()
	{
		StartCoroutine ("die");
	}

	public IEnumerator die(){
		Debug.Log ("enemyDie");
		lives -= 1;
		if (lives <= 0) {
						this.GetComponent<Rigidbody2D>().velocity = new Vector2 (0, 0);
						anim.SetBool ("Dead", true);
						Dead = true;				
						anim.SetBool ("Attacking", false);
				
						yield return new WaitForSeconds (1.0f);
						Destroy (this.gameObject);
				}
		}
	
	public virtual IEnumerator attack(){
		yield return new WaitForSeconds (1.0f);
		anim.SetBool ("Attacking", false);
		if (Physics2D.OverlapCircle (PlayerCheck.position, attackRadius, whatIsPlayer)) {
			playerscript.Die ();
		}
		
	}

	protected virtual void OnCollisionEnter2D(Collision2D collision){
		if(collision.gameObject.name == "character")
		this.GetComponent<Rigidbody2D>().velocity = new Vector2(0, this.GetComponent<Rigidbody2D>().velocity.y);
	}

	protected void OnCollisionStay2D(Collision2D collision){
		if (collision.gameObject.name == "character" || collision.gameObject.tag == "Enemy") {
						this.GetComponent<Rigidbody2D>().velocity = new Vector2 (0, this.GetComponent<Rigidbody2D>().velocity.y);
						this.GetComponent<Rigidbody2D>().isKinematic = true;
				}
		}

	protected void OnCollisionExit2D(Collision2D collision){
		if (collision.gameObject.name == "character" || collision.gameObject.tag == "Enemy") {
						this.GetComponent<Rigidbody2D>().isKinematic = false;
				}
		}
	protected virtual IEnumerator Activate(){
		yield return new WaitForSeconds (0.05f);
		inview = Camera.main.WorldToViewportPoint (this.transform.position);
		if (inview.x <= 1 && inview.x >= 0 && !activated) {
			activated = true;
		}
		}
	protected virtual void MainLoopCode(){

		}
		
	}