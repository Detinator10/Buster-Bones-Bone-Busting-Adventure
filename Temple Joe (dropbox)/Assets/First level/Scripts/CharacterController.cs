using UnityEngine;
using System.Collections;

public class CharacterController : MonoBehaviour {
	public float maxSpeed = 5f;
	bool facingRight = true;
	Animator anim;
	bool grounded = false;
	public Transform groundCheck;
	float groundRadius = 0.2f;
	public LayerMask whatIsGround;
	public float jumpForce = 1000f;
	public GameObject DeathHeight;
	public bool Dead = false;
	public GameObject GameOver;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		DeathHeight = GameObject.Find("Death Elevation");
		
	}
	void Update()
	{
		if (!Dead) {
						//Change Input.GetKeyDown and instead use Input manager
						if (grounded && Input.GetKeyDown (KeyCode.Space)) {
								anim.SetBool ("Ground", false);
								GetComponent<Rigidbody2D>().AddForce (new Vector2 (0, jumpForce));
						}
				}
	}
	// Update is called once per frame
	void FixedUpdate () {
		if (!Dead) {
						grounded = Physics2D.OverlapCircle (groundCheck.position, groundRadius, whatIsGround);
						anim.SetBool ("Ground", grounded);
						anim.SetFloat ("vSpeed", GetComponent<Rigidbody2D>().velocity.y);
						float move = Input.GetAxis ("Horizontal");
						anim.SetFloat ("Speed", Mathf.Abs (move));
						GetComponent<Rigidbody2D>().velocity = new Vector2 (move * maxSpeed, GetComponent<Rigidbody2D>().velocity.y);
						if (move > 0 && !facingRight)
								Flip ();

						if (move < 0 && facingRight)
								Flip ();

						if (this.transform.position.y <= DeathHeight.transform.position.y)
								Dead = true;
				}

						if (Dead)
								Die ();
				
	}
	void Flip()
	{
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

	void Die()
	{

	}
}
