using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerControllerScript : MonoBehaviour
{

    [HideInInspector]
    public bool facingRight = true;
    [HideInInspector]
    public bool jump = false;
    public float moveForce = 365f;
    public float maxSpeed = 30f;
    public float jumpForce = 1000f;
    public Transform groundCheck;


    private bool grounded = false;
    private Animator anim;
    private Rigidbody2D rb2d;
    protected bool attack = false;
    public Transform EnemyCheck1;
    public Transform EnemyCheck2;
    public LayerMask whatIsEnemy;
    protected Collider2D[] EnemiesInRange;
    protected float minvalue = 0f;
    protected GameObject closestEnemy;
    public int damage = 1;
    public Text coinScoretext;
    public int coinScore = 0;
    public bool Dead = false;
    public int health = 1;
    public LayerMask whatIsGround;
    private IEnumerator coroutine;
    public Transform EndPoint;
    public GameManager GM;
    public Transform Deathheight;
    public Text Lives;
    public Text score;
    bool dying = false;
    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        GM = GameObject.Find("_GM").GetComponent<GameManager>();
    }
    void Awake()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(this.transform.position.y <= Deathheight.position.y)
        {
            Die(0);
        }
        grounded = Physics2D.Linecast(transform.position, groundCheck.position, whatIsGround);

        if (Input.GetButtonDown("Fire1"))
        {
            anim.SetBool("Attacking", true);
            attack = true;
        }
        if (Input.GetButtonDown("Jump") && grounded)
        {
            jump = true;
        }
        coinScoretext.text = coinScore.ToString();
        score.text = GM.score[GameManager.player].ToString();
        Lives.text = GM.lives.ToString();
    }

    void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");
        if(h < 0)
        {
            h = -1;
        } else if(h > 0)
        {
            h = 1;
        }
        anim.SetFloat("Velocity", Mathf.Abs(h));
        if(!dying)
        rb2d.velocity = new Vector2(maxSpeed*h, rb2d.velocity.y);

        if (h > 0 && !facingRight)
            Flip();
        else if (h < 0 && facingRight)
            Flip();

        if (jump)
        {
            anim.SetTrigger("Jump");
            rb2d.velocity = new Vector2(rb2d.velocity.x, 0);
            rb2d.AddForce(new Vector2(0f, jumpForce));
            jump = false;
        }

        if (attack)
        {

            if (Physics2D.OverlapArea(EnemyCheck1.position, EnemyCheck2.position, whatIsEnemy))
            {

                EnemiesInRange = Physics2D.OverlapAreaAll(EnemyCheck1.position, EnemyCheck2.position, whatIsEnemy);
                foreach(Collider2D enemy in EnemiesInRange)
                {
                    if (minvalue == 0f) {
                        minvalue = Mathf.Abs(enemy.transform.position.x - this.transform.position.x);
                        closestEnemy = enemy.gameObject;
                            }
                    else if (Mathf.Abs(enemy.transform.position.x) < minvalue)
                    {
                        minvalue = Mathf.Abs(enemy.transform.position.x - this.transform.position.x);
                        closestEnemy = enemy.gameObject;
                    }
                }
                if (closestEnemy != null)
                {
                    closestEnemy.GetComponent<EnemyController>().Damage(damage);
                    GM.score[GameManager.player] += 100;
                }
                minvalue = 0;
            }
            attack = false;
            coroutine = AttackAnim();
            StartCoroutine(coroutine);
        }
        if(this.transform.position.x > EndPoint.position.x)
        {
            coroutine = NextLevel();
            StartCoroutine(coroutine);
        }
    }

    IEnumerator AttackAnim()
    {
        yield return new WaitForSeconds(.4f);
        anim.SetBool("Attacking", false);
    }
    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
    public void Die(int deathTime)
    {
        if (dying == false)
        {
            coroutine = die(deathTime);
            StartCoroutine(coroutine);
        }
    }

    protected IEnumerator die(int deathTime)
    {
        anim.SetBool("Dead", true);
        GM.lives -= 1;
        dying = true;
        yield return new WaitForSeconds(deathTime);
        if (GM.lives > 0)
        {
            dying = false;
            GM.LoadScene("GameOver");
        }
        else if (GM.lives <= 0)
        {
            
            GM.lives = 3;
            float fadeTime = this.GetComponent<FadingScript>().BeginFade(1);
            yield return new WaitForSeconds(fadeTime);
            dying = false;
            GM.LoadScene("GameOverF");
        }
    }
    public IEnumerator NextLevel()
    {

        float fadeTime = this.GetComponent<FadingScript>().BeginFade(1);
        yield return new WaitForSeconds(fadeTime);
        GM.LoadScene(EndPoint.GetComponent<endpoint>().level);
        
    }

}