using UnityEngine;
using System.Collections;

public class ChargerEnemyScript : EnemyController {

    [SerializeField]
    protected bool playerCollision = false;
    [SerializeField]
    protected float chargeMove;
    protected float newChargeMove;
    override protected IEnumerator Attack(int x)
    {
        attacking = true;
        animController.SetBool("Attacking", true);
        yield return new WaitForSeconds(.5f);
        float time = 0f;
        this.gameObject.layer = 12;
        if (!facingRight)
        {
            newmove = chargeMove;

        }
        else if (facingRight)
        {
            newmove = chargeMove * -1;

        }

        while (!playerCollision&&time<1)
        {
            time += Time.deltaTime;
            GetComponent<Rigidbody2D>().velocity = new Vector2(newmove, GetComponent<Rigidbody2D>().velocity.y);
            yield return null;
        }
        playerCollision = false;
        attacking = false;
        animController.SetBool("Attacking", false);
        this.gameObject.layer = 11;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.tag == "Player")
        {
            playerCollision = true;
            Player.GetComponent<PlayerControllerScript>().Die(3);
        }
    }
}
