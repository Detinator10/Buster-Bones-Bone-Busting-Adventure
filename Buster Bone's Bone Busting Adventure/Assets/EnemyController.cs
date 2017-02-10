using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour
{
    protected float newmove;
    protected float originalmove = 2.5f;
    public GameObject Player;
    protected bool facingRight = false;
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
    protected Vector3 BackupPosition;
    protected Animator anim;

    public bool dead;

    public bool activated = false;
    protected Vector3 inview;
    protected bool pushed = false;
    protected bool onMovingPlatform;
    public LayerMask whatIsMovingPlatform;
    protected Collider2D movingplat;
    public bool exception;
    protected Vector3 theScale;
    protected bool PlayerInRange;
    [SerializeField]
    protected bool attacking = false;
    protected IEnumerator coroutine;
    public Vector2 playercheck1;
    public Vector2 playercheck2;
    private int health=1;
    protected Animator animController;
    // Use this for initialization
    void Start()
    {
        playercheck1 = this.transform.FindChild("PlayerCheck1").transform.position;
        playercheck2 = this.transform.FindChild("PlayerCheck2").transform.position;
        BackupPosition = this.transform.position;
        animController = this.GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    protected virtual void FixedUpdate()
    {
        if (activated)
        {
            playercheck1 = this.transform.FindChild("PlayerCheck1").transform.position;
            playercheck2 = this.transform.FindChild("PlayerCheck2").transform.position;
            animController.SetFloat("Velocity", Mathf.Abs(this.GetComponent<Rigidbody2D>().velocity.x));
            if (this.transform.position.x < Player.transform.position.x && !attacking)
            {
                newmove = originalmove;

            }
            if (this.transform.position.x > Player.transform.position.x && !attacking)
            {
                newmove = originalmove * -1;

            }

            if (!attacking)
                GetComponent<Rigidbody2D>().velocity = new Vector2(newmove, GetComponent<Rigidbody2D>().velocity.y);

            //anim.SetFloat("Speed", Mathf.Abs(newmove));
            if (newmove < 0 && !facingRight && !attacking)
                Flip();

            if (newmove > 0 && facingRight && !attacking)
                Flip();


            if (Physics2D.OverlapArea(playercheck1, playercheck2, whatIsPlayer.value) && !attacking)
            {
                coroutine = Attack(1);
                StartCoroutine(coroutine);
            }



            grounded = Physics2D.OverlapCircle(GroundCheck.position, groundRadius, whatIsGround);
            if (!grounded)
            {
                ifnotgrounded();
            }

        }
    }

    protected virtual void ifnotgrounded()
    {
    }
    protected virtual void Flip()
    {
        facingRight = !facingRight;
        theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
    virtual protected IEnumerator Attack(int x)
    {
        attacking = true;

        yield return new WaitForSeconds(x);
        animController.SetBool("Attacking", true);
        yield return new WaitForSeconds(.5f);
        if (Physics2D.OverlapArea(playercheck1,playercheck2,whatIsPlayer.value))
        {
            Player.GetComponent<PlayerControllerScript>().Die(3);

        }

            attacking = false;
        animController.SetBool("Attacking", false);

    }

    virtual public void Damage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }
    protected void Die()
    {
        DestroyImmediate(this.gameObject);
    }
    public void activate()
    {
        activated = true;

    }
}

