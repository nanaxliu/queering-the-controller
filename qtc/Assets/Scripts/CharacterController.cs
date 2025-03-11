using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;

public class CharacterController : MonoBehaviour
{
    public int PlayerNumber;

    public GameManager gameManager;

    public float speed;
    public float jumpSpeed;
    public float attackForce;
    public Animator animator;
    public SpriteRenderer sprite;

    private static KeyCode[] p1inputs = { KeyCode.W , KeyCode.A, KeyCode.S, KeyCode.D};
    private static KeyCode[] p2inputs = { KeyCode.LeftArrow , KeyCode.RightArrow, KeyCode.UpArrow, KeyCode.DownArrow};
    
    public KeyCode p1Button1 = p1inputs.ElementAt(0);
    public KeyCode p1Button2 = p1inputs.ElementAt(1);
    public KeyCode p1Button3 = p1inputs.ElementAt(2);
    public KeyCode p1Button4 = p1inputs.ElementAt(3);

    public KeyCode p2Button1 = p2inputs.ElementAt(0);
    public KeyCode p2Button2 = p2inputs.ElementAt(1);
    public KeyCode p2Button3 = p2inputs.ElementAt(2);
    public KeyCode p2Button4 = p2inputs.ElementAt(3);

    bool canJump;

    public Rigidbody2D rb;

    Vector2 directionToOtherPlayer;

    public Camera playerCamera;
    public float score;
    public TextMeshProUGUI scoreText;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        canJump = true;
        gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
            score = Mathf.RoundToInt((transform.position.y + 2.63f)*10);
            scoreText.text = score.ToString();

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

        if (Input.GetKeyDown(KeyCode.Z) && gameManager.silentMode)
        {
            Debug.Log("sound detected");

            shuffleP1Inputs();
            shuffleP2Inputs();

            gameManager.ChangeState(GameState.ScreenShake);
        }
        
        if(gameManager.gameState != GameState.ScreenShake || (playerCamera.transform.position.y - transform.position.y) > 2) //keeps the camera following the player except for when the platforms shake. might refine this with a coroutine to have the camera catch up w/ the player b/c its kinda clunky rn
        {
            playerCamera.transform.position = new Vector3(transform.position.x, transform.position.y +1.63f, -10);
        }


    }

    void P1Movement()
    {

        if (Input.GetKeyDown(p1Button2))
        {
            animator.SetBool("isWalking", true);
            sprite.flipX = true;
            rb.AddForce( new Vector2(-speed, 0), ForceMode2D.Impulse);
        }

        if (Input.GetKeyUp(p1Button2)) 
        {
			animator.SetBool("isWalking", false);
		}

        if (Input.GetKeyDown(p1Button4))
        {
            animator.SetBool("isWalking", true);
            sprite.flipX = false;
            rb.AddForce(new Vector2(speed, 0), ForceMode2D.Impulse);
        }

        if (Input.GetKeyUp(p1Button4)) 
        {
			animator.SetBool("isWalking", false);
		}

        if (Input.GetKeyDown(p1Button1) && canJump)
        {
            rb.AddForce(new Vector2(0, jumpSpeed), ForceMode2D.Impulse);
            canJump = false;
        }

        if (Input.GetKeyDown(p1Button3))
        {
            animator.SetTrigger("kicked");
        }
    }

    void P2Movement()
    {
        if (Input.GetKey(p2Button1))
        {
            animator.SetBool("p2isWalking", true);
            sprite.flipX = true;
            rb.AddForce(new Vector2(-speed, 0), ForceMode2D.Impulse);
        }

        if (Input.GetKeyUp(p2Button1)) 
        {
			animator.SetBool("p2isWalking", false);
		}

        if (Input.GetKey(p2Button2))
        {
            animator.SetBool("p2isWalking", true);
            sprite.flipX = false;
            rb.AddForce(new Vector2(speed, 0), ForceMode2D.Impulse);
        }

        if (Input.GetKeyUp(p2Button2)) 
        {
			animator.SetBool("p2isWalking", false);
		}

        if (Input.GetKeyDown(p2Button3) && canJump);
        {
            rb.AddForce(new Vector2(0, jumpSpeed), ForceMode2D.Impulse);

            canJump = false;
        }

        if (Input.GetKeyDown(p2Button4))
        {
            animator.SetTrigger("p2kicked");
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            directionToOtherPlayer = transform.position - other.transform.position;

            if (PlayerNumber == 1 && Input.GetKey(p1Button3))
            {
                if (directionToOtherPlayer.x <= 0) // checks if they're to the left
                {
                    other.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(attackForce, 0), ForceMode2D.Impulse);
                }
                else { other.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(-attackForce, 0), ForceMode2D.Impulse); } // checks if they're to the right
            }

            if (PlayerNumber == 2 && Input.GetKey(p2Button4))
            {
                if (directionToOtherPlayer.x <= 0) // checks if they're to the left
                {
                    other.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(attackForce, 0), ForceMode2D.Impulse);
                }
                else { other.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(-attackForce, 0), ForceMode2D.Impulse); } // checks if they're to the right
            }
        }
    }


    public void shuffleP1Inputs()
    {
        for (int t = 0; t < p1inputs.Length; t++ )
        {
            KeyCode tmp = p1inputs[t];
            int r = Random.Range(t, p1inputs.Length);
            p1inputs[t] = p1inputs[r];
            p1inputs[r] = tmp;
        }

        p1Button1 = p1inputs.ElementAt(0);
        p1Button2 = p1inputs.ElementAt(1);
        p1Button3 = p1inputs.ElementAt(2);
        p1Button4 = p1inputs.ElementAt(3);

    }

    public void shuffleP2Inputs()
    {
        for (int t = 0; t < p2inputs.Length; t++ )
        {
            KeyCode tmp2 = p2inputs[t];
            int r = Random.Range(t, p2inputs.Length);
            p2inputs[t] = p2inputs[r];
            p2inputs[r] = tmp2;
        }

        p2Button1 = p2inputs.ElementAt(0);
        p2Button2 = p2inputs.ElementAt(1);
        p2Button3 = p2inputs.ElementAt(2);
        p2Button4 = p2inputs.ElementAt(3);

    }
}
