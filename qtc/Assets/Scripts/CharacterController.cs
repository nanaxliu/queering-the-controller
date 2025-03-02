using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public int PlayerNumber;

    public float speed;
    public float jumpSpeed;
    public float attackForce;
    public Animator animator;
    public SpriteRenderer sprite;

    bool canJump;
    bool canAttack;

    public Rigidbody2D rb;

    Vector2 directionToOtherPlayer;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        canJump = true;
        canAttack = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (PlayerNumber == 1)
        {
            P1Movement();
        }

        if(PlayerNumber == 2)
        {
            P2Movement();
        }

        if (rb.velocity.y == 0)
        {
            canJump = true;
        }
    }

    void P1Movement()
    {

        if (Input.GetKey(KeyCode.A))
        {
            animator.SetBool("isWalking", true);
            sprite.flipX = true;
            rb.AddForce( new Vector2(-speed, 0), ForceMode2D.Impulse);
        }

        if (Input.GetKeyUp(KeyCode.A)) 
        {
			animator.SetBool("isWalking", false);
		}

        if (Input.GetKey(KeyCode.D))
        {
            animator.SetBool("isWalking", true);
            sprite.flipX = false;
            rb.AddForce(new Vector2(speed, 0), ForceMode2D.Impulse);
        }

        if (Input.GetKeyUp(KeyCode.D)) 
        {
			animator.SetBool("isWalking", false);
		}

        if (Input.GetKeyDown(KeyCode.W) && canJump)
        {
            rb.AddForce(new Vector2(0, jumpSpeed), ForceMode2D.Impulse);
            canJump = false;
        }

        if (Input.GetKeyDown(KeyCode.S) && canAttack)
        {
            animator.SetTrigger("kicked");
        }
    }

    void P2Movement()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            animator.SetBool("isWalking", true);
            sprite.flipX = true;
            rb.AddForce(new Vector2(-speed, 0), ForceMode2D.Impulse);
        }

        if (Input.GetKeyUp(KeyCode.LeftArrow)) 
        {
			animator.SetBool("isWalking", false);
		}

        if (Input.GetKey(KeyCode.RightArrow))
        {
            animator.SetBool("isWalking", true);
            sprite.flipX = false;
            rb.AddForce(new Vector2(speed, 0), ForceMode2D.Impulse);
        }

        if (Input.GetKeyUp(KeyCode.RightArrow)) 
        {
			animator.SetBool("isWalking", false);
		}

        if (Input.GetKeyDown(KeyCode.UpArrow) && canJump)
        {
            rb.AddForce(new Vector2(0, jumpSpeed), ForceMode2D.Impulse);

            canJump = false;
        }

        if (Input.GetKeyDown(KeyCode.DownArrow) && canAttack)
        {
            animator.SetTrigger("kicked");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")){
            canAttack = true;
        }
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            directionToOtherPlayer = transform.position - other.transform.position;

            if (PlayerNumber == 1 && Input.GetKey(KeyCode.S))
            {
                if (directionToOtherPlayer.x <= 0) // checks if they're to the left
                {
                    other.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(attackForce, 0), ForceMode2D.Impulse);
                }
                else { other.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(-attackForce, 0), ForceMode2D.Impulse); } // checks if they're to the right
            }

            if (PlayerNumber == 2 && Input.GetKey(KeyCode.DownArrow))
            {
                if (directionToOtherPlayer.x <= 0) // checks if they're to the left
                {
                    other.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(attackForce, 0), ForceMode2D.Impulse);
                }
                else { other.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(-attackForce, 0), ForceMode2D.Impulse); } // checks if they're to the right
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player")){
            canAttack = false; }
    }
}
