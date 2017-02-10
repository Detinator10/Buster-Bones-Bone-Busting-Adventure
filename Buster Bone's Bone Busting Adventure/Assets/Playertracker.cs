using UnityEngine;
using System.Collections;


public class Playertracker : MonoBehaviour {
	public float xMargin = 1f;
	public float yMargin = 1f;
	public float xSmooth = 8f;
	public float ySmooth = 8f;
	public Vector2 maxXandY;
	public Vector2 minXandY;
	public float targetX;
	public float targetY;
	float camx;
	float camy;
	float midX;
	float midY;
	public Transform DeathHeight;

	private Transform Player;

    public Transform enemyCheck1;
    public Transform enemyCheck2;
    public LayerMask whatIsEnemy;
    protected Collider2D[] EnemiesInRange;

    void Start(){
		Screen.SetResolution (1366, 768, true);
		midX = this.GetComponent<Camera>().pixelWidth / 2;
		midY = this.GetComponent<Camera>().pixelHeight / 2;
	}
	void Awake ()
	{
		Player = GameObject.FindGameObjectWithTag ("Player").transform;
	}

	bool CheckYMargin()
	{
		return Mathf.Abs (transform.position.y - Player.position.y) > yMargin;
	}

	bool CheckXMargin()
	{
		return Mathf.Abs (transform.position.x - Player.position.x) > xMargin;
	}

	void FixedUpdate ()
	{
		if(Player.transform.position.y > DeathHeight.position.y){
		TrackPlayer ();
		}
        if (Physics2D.OverlapArea(enemyCheck1.position, enemyCheck2.position, whatIsEnemy.value))
        {
            EnemiesInRange = Physics2D.OverlapAreaAll(enemyCheck1.position, enemyCheck2.position, whatIsEnemy);
            foreach (Collider2D enemy in EnemiesInRange)
            {
                if (enemy != null&& enemy.GetComponent<EnemyController>() !=null)
                {
                    enemy.GetComponent<EnemyController>().activate();
                }
            }
        }
    }

	void TrackPlayer()
	{
		targetX = transform.position.x;
		targetY = transform.position.y;

		if (CheckXMargin ())
			targetX = Mathf.Lerp (transform.position.x, Player.position.x, xSmooth * Time.deltaTime);

		targetX = Mathf.Clamp (targetX, minXandY.x, maxXandY.x);

		transform.position = new Vector3 (targetX, targetY, transform.position.z);
	}

	public float Positionx(){
		camx = this.transform.position.x + midX;

		return camx;
		}
	public float Positiony(){
		camy = this.transform.position.y + midY;

		return camy;
		}

}
