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

    public Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
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

        if (Input.GetKeyDown(KeyCode.W))
        {
            rb.AddForce(new Vector2(0, jumpSpeed), ForceMode2D.Impulse);
        }

        if (Input.GetKeyDown(KeyCode.S))
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

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            rb.AddForce(new Vector2(0, jumpSpeed), ForceMode2D.Impulse);
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            animator.SetTrigger("kicked");
        }
    }
}
