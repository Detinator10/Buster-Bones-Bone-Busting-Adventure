using UnityEngine;
using System.Collections;

public class Boss : EnemyScript {
	// Update is called once per frame
	protected override void FixedUpdate () {
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

		
		if(!anim.GetBool("Dead") && playerscript.Dead == false && !anim.GetBool("Attacking")  && activated && !exception)
		{
			if (this.transform.position.x < Player.transform.position.x){
				newmove = originalmove;
				
			}
			if (this.transform.position.x > Player.transform.position.x){
				newmove = originalmove * -1;
				
			}
			
			GetComponent<Rigidbody2D>().velocity = new Vector2 (newmove, GetComponent<Rigidbody2D>().velocity.y);
			if(grounded){
			GetComponent<Rigidbody2D>().AddForce(new Vector2 (0,600));
			}
			
			anim.SetFloat("Speed",Mathf.Abs(newmove));
			if (newmove < 0 && !facingRight)
				Flip ();
			
			if (newmove > 0 && facingRight)
				Flip ();
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
	void Hop(){
		Debug.Log ("Hop");
		if (grounded) {
			GetComponent<Rigidbody2D>().AddForce (new Vector2 (newmove, 2000));
		}
	}
	protected override void ifnotgrounded(){

		}
	protected override IEnumerator Activate(){
		yield return new WaitForSeconds (5f);
		inview = Camera.main.WorldToViewportPoint (this.transform.position);
		if (inview.x <= 1 && inview.x >= 0 && !activated) {
			activated = true;
		}
	}


}
