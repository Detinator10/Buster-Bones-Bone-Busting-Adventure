using UnityEngine;
using System.Collections;

public class ArcherScript : EnemyScript {
	public float distanceToPlayer;
	public float range;
	public bool canAttack = false;
	public GameObject bow;
	public GameObject arrow;
	public GameObject rightarrow;
	public float shootforce;
	protected GameObject thisproj;
	public GameObject head;
	public bool waited;
	public bool firstshot = true;	
	//Vector3 scale;
	public bool shouldflip;
	public LookScript headscript;
	public float waitTime;
	public float StartwaitTime;


	// Use this for initialization
	protected override void Start ()
	{
		base.Start ();
		//headscript = transform.FindChild ("head").GetComponent<LookScript>();
		//scale = transform.localScale;
	}
	
	// Update is called once per frame
	protected override void FixedUpdate () {
		distanceToPlayer = Vector2.Distance (this.transform.position, Player.transform.position);
		base.FixedUpdate ();
		if (anim.GetBool ("Attacking") && newmove > 0 && facingRight) {
			Flip ();
				}
		if (anim.GetBool ("Attacking") && newmove < 0 && !facingRight) {
			Flip ();
				}
	}

public override IEnumerator attack ()
	{
						Debug.Log ("Attack");
		if (firstshot) {

						yield return new WaitForSeconds (StartwaitTime);
			anim.SetBool ("Attacking", false);
			if(playerscript.Dead == false && !anim.GetBool("Dead")){
				if(InRange()){
						canAttack = false;
				thisproj = Instantiate (arrow, bow.transform.position, head.transform.rotation) as GameObject;
						if (!headscript.facingLeft) {
								
								thisproj.GetComponent<Rigidbody2D>().AddForce (-bow.transform.right * shootforce);
						} else if (headscript.facingLeft) {
								thisproj.GetComponent<Rigidbody2D>().AddForce (-bow.transform.right * shootforce);
						}
					//firstshot = false;
			}
			}
			firstshot = false;
		} 
		else {
			if(playerscript.Dead == false && !anim.GetBool("Dead")){
				if(InRange()){
					Debug.Log ("x");
						canAttack = false;
				thisproj = Instantiate (arrow, bow.transform.position, head.transform.rotation) as GameObject;
						if (headscript.facingLeft) {

					thisproj.GetComponent<Rigidbody2D>().AddForce (-bow.transform.right * shootforce);
						} else if (!headscript.facingLeft) {
					thisproj.GetComponent<Rigidbody2D>().AddForce (-bow.transform.right * shootforce);
						}

						yield return new WaitForSeconds (waitTime);
			}
				}

			anim.SetBool ("Attacking", false);
				}

						canAttack = true;
				


	}

	protected override void MainLoopCode ()
	{
		if (InRange() && canAttack == true) {
			Attack();
		}
	}
	protected override void Flip ()
	{

		//transform.FindChild ("head").transform.localScale = scale;
	}

	protected bool InRange(){
		distanceToPlayer = Vector2.Distance (this.transform.position, Player.transform.position);
		if (distanceToPlayer <= range) {
						return true;
				} else {
			return false;
				}
	}
}
