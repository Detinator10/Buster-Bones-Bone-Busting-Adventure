using UnityEngine;
using System.Collections;

public class EnemyChargerScript : EnemyScript {
	public int chargeSpeed;
	public GameObject deathHeight;
	public bool justattacking;







	
	// Use this for initialization

	// Update is called once per fram
	protected override void Start ()
	{
		base.Start ();
		lives = 2;
	}
	protected override void FixedUpdate () {
		if (exception && grounded) {
			exception = false;
			anim.SetBool("Attacking", false);
				}
		base.FixedUpdate ();
		if (!anim.GetBool ("Dead") && playerscript.Dead == false && !anim.GetBool ("Attacking") && activated) {
			if(Physics2D.OverlapArea (PlayerCheck.position,PlayerCheck2.position, whatIsPlayer, -20, 20))
				Attack ();
		}
		
	}
	
	protected override void ifnotgrounded ()
	{
		if (!anim.GetBool("Attacking") && !grounded) {
						base.ifnotgrounded ();
				} 
	}
	public override IEnumerator attack(){



		yield return new WaitForSeconds (1.5f);
		Debug.Log ("kill");
		if(!anim.GetBool("Dead")){
		if (facingRight) {
						GetComponent<Rigidbody2D>().AddForce (new Vector2 (chargeSpeed * -1, 0));
				}
		else {
			GetComponent<Rigidbody2D>().AddForce(new Vector2 (chargeSpeed, 0));
				}
		Debug.Log ("x");
			this.GetComponent<Rigidbody2D>().velocity = new Vector2(0, this.GetComponent<Rigidbody2D>().velocity.y);

		yield return new WaitForSeconds (0.75f);
			if(grounded){
			anim.SetBool("Attacking", false);
			}
			else{
				exception = true;
			}
			
		}

	}

	protected override void OnCollisionEnter2D(Collision2D collision){
		base.OnCollisionEnter2D(collision);
		if (collision.gameObject.name == "character" && !anim.GetBool ("Dead")) {
						playerscript.Die ();
				
				}
	}
}