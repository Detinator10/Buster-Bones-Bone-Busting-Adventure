using UnityEngine;
using System.Collections;

public class ArcherSkeleton : EnemyController
{

    bool firstshot = true;
    public PlayerControllerScript player;
    public float distanceToPlayer;
    public float range;
    public float StartwaitTime;
    protected bool canAttack = true;
    public float lookangle;
    public bool facingLeft;
    public GameObject sprite;
    public float rotateZ = 0.0f;
    public GameObject arrow;
    protected GameObject thisproj;
    public float waitTime;
    public float shootforce;
    public Transform shootLoc;

    
    // Use this for initialization
    void Start()
    {
        animController = this.transform.parent.GetComponent<Animator>();
    }
    protected override void FixedUpdate()
    {
        if (InRange()&& canAttack)
        {
            coroutine = Attack(1);
            StartCoroutine(coroutine);
        }
    }
    protected override IEnumerator Attack(int x)
    {
        canAttack = false;

        Debug.Log("Attack");

            if (player.Dead == false)
            {
                if (InRange())
                {
                yield return new WaitForSeconds(waitTime);
                Debug.Log("x");
                animController.SetBool("Attacking", true);
                yield return new WaitForSeconds(.5f);
                canAttack = false;
                    thisproj = Instantiate(arrow, shootLoc.position, transform.rotation) as GameObject;
                    if (facingLeft)
                    {

                        thisproj.GetComponent<Rigidbody2D>().AddForce(-transform.right * shootforce);
                    }
                    else if (!facingLeft)
                    {
                        thisproj.GetComponent<Rigidbody2D>().AddForce(-transform.right * shootforce);
                    }
                yield return new WaitForSeconds(.5f);
                animController.SetBool("Attacking", false);

            }
            }

        

        canAttack = true;



    }
    protected bool InRange()
    {
        distanceToPlayer = Vector2.Distance(this.transform.position, Player.transform.position);
        if (distanceToPlayer <= range)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    // Update is called once per frame
    void Update()
    {
        var dx = this.transform.position.x - player.transform.position.x;
        var dy = this.transform.position.y - player.transform.position.y;
        var radians = Mathf.Atan2(dy, dx);
        var angle = radians * 180.0f / Mathf.PI; //I dont't know what is variable lookangle, but maybe you want translate radians to degree
        rotateZ = Mathf.LerpAngle(rotateZ, angle, 5.0f * Time.deltaTime);

        this.transform.rotation = Quaternion.Euler(0, 0, rotateZ);
        //var dir = player.transform.position - transform.position;
        //var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        //angle -= 180;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        if (player.transform.position.x > transform.position.x && facingLeft)
        {
            facingLeft = false;
            var scale = transform.localScale;
            scale.y *= -1;
            transform.localScale = scale;
            scale = transform.localScale;
            //scale.x *= -1;
            //sprite.transform.localScale = scale;
        }
        else if (player.transform.position.x < transform.position.x && !facingLeft)
        {
            facingLeft = true;
            var scale = transform.localScale;
            scale.y = -scale.y;
            this.transform.localScale = scale;
            scale = sprite.transform.localScale;
            //scale.x *= -1;
            //sprite.transform.localScale = scale;
        }
    }
}
