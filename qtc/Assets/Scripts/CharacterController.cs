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
    public Animator p2animator;
    public SpriteRenderer sprite;

    public static KeyCode[] p1inputs = {KeyCode.W , KeyCode.A, KeyCode.S, KeyCode.D};
    public static KeyCode[] p2inputs = {KeyCode.LeftArrow , KeyCode.RightArrow, KeyCode.UpArrow, KeyCode.DownArrow};

    public KeyCode p1ButtonJump = p1inputs.ElementAt(0); //W Red
    public KeyCode p1ButtonLeft = p1inputs.ElementAt(1); //A Yellow
    public KeyCode p1ButtonKick = p1inputs.ElementAt(2); //S Blue
    public KeyCode p1ButtonRight = p1inputs.ElementAt(3); //D Green

    public KeyCode p2ButtonLeft = p2inputs.ElementAt(0); //Left Yellow
    public KeyCode p2ButtonRight = p2inputs.ElementAt(1); //Right Green
    public KeyCode p2ButtonJump = p2inputs.ElementAt(2); //Up Red
    public KeyCode p2ButtonKick = p2inputs.ElementAt(3); //Down Blue

    public TextMeshProUGUI[] p1labels;
    public TextMeshProUGUI[] p2labels;

    public AudioSource jumpSFX;
    public AudioSource hitSFX;

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
            
            if(PlayerNumber == 1)
            {
                shuffleP1Inputs();
            }

            if(PlayerNumber == 2)
            {
                Debug.Log("player 2 shuffling");
                shuffleP2Inputs();
                Invoke("PingGameManager", .001f);
            }
        }
        
        if(gameManager.gameState != GameState.ScreenShake || (playerCamera.transform.position.y - transform.position.y) > 2) //keeps the camera following the player except for when the platforms shake. might refine this with a coroutine to have the camera catch up w/ the player b/c its kinda clunky rn
        {
            playerCamera.transform.position = new Vector3(transform.position.x, transform.position.y +1.63f, -10);
        }


    }

    void PingGameManager()
    {
        gameManager.ChangeState(GameState.ScreenShake);
    }

    void P1Movement()
    {

        if (Input.GetKeyDown(p1ButtonLeft))
        {
            animator.SetBool("isWalking", true);
            sprite.flipX = true;
            rb.AddForce( new Vector2(-speed, 0), ForceMode2D.Impulse);
        }

        if (Input.GetKeyUp(p1ButtonLeft)) 
        {
			animator.SetBool("isWalking", false);
		}

        if (Input.GetKeyDown(p1ButtonRight))
        {
            animator.SetBool("isWalking", true);
            sprite.flipX = false;
            rb.AddForce(new Vector2(speed, 0), ForceMode2D.Impulse);
        }

        if (Input.GetKeyUp(p1ButtonRight)) 
        {
			animator.SetBool("isWalking", false);
		}

        if (Input.GetKeyDown(p1ButtonJump) && canJump)
        {
            rb.AddForce(new Vector2(0, jumpSpeed), ForceMode2D.Impulse);
            jumpSFX.Play();
            canJump = false;
        }

        if (Input.GetKeyDown(p1ButtonKick))
        {
            animator.SetTrigger("kicked");
        }
    }

    void P2Movement()
    {

        if (Input.GetKeyDown(p2ButtonLeft))
        {
            p2animator.SetBool("p2isWalking", true);
            sprite.flipX = true;
            rb.AddForce(new Vector2(-speed, 0), ForceMode2D.Impulse);
        }

        if (Input.GetKeyUp(p2ButtonLeft)) 
        {
			p2animator.SetBool("p2isWalking", false);
		}

        if (Input.GetKeyDown(p2ButtonRight))
        {
            p2animator.SetBool("p2isWalking", true);
            sprite.flipX = false;
            rb.AddForce(new Vector2(speed, 0), ForceMode2D.Impulse);
        }

        if (Input.GetKeyUp(p2ButtonRight)) 
        {
			p2animator.SetBool("p2isWalking", false);
		}

        if (Input.GetKeyDown(p2ButtonJump) && canJump)
        {
            rb.AddForce(new Vector2(0, jumpSpeed), ForceMode2D.Impulse);
            jumpSFX.Play();
            canJump = false;
        }

        if (Input.GetKeyDown(p2ButtonKick))
        {
            p2animator.SetTrigger("p2kicked");
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            directionToOtherPlayer = transform.position - other.transform.position;

            if (PlayerNumber == 1 && Input.GetKey(p1ButtonKick))
            {
                hitSFX.Play();
                p2animator.SetTrigger("p2hissed");
                if (directionToOtherPlayer.x <= 0) // checks if they're to the left
                {
                    other.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(attackForce, 0), ForceMode2D.Impulse);
                }
                else { other.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(-attackForce, 0), ForceMode2D.Impulse); } // checks if they're to the right
            }

            if (PlayerNumber == 2 && Input.GetKey(p2ButtonKick))
            {
                hitSFX.Play();
                animator.SetTrigger("hissed");
                if (directionToOtherPlayer.x <= 0) // checks if they're to the left
                {
                    other.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(attackForce, 0), ForceMode2D.Impulse);
                }
                else { other.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(-attackForce, 0), ForceMode2D.Impulse); } // checks if they're to the right
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            directionToOtherPlayer = transform.position - other.transform.position;

            if (PlayerNumber == 1 && Input.GetKeyDown(p1ButtonKick))
            {
                hitSFX.Play();
                p2animator.SetTrigger("p2hissed");
                if (directionToOtherPlayer.x <= 0) // checks if they're to the left
                {
                    other.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(attackForce, 0), ForceMode2D.Impulse);
                }
                else { other.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(-attackForce, 0), ForceMode2D.Impulse); } // checks if they're to the right
            }

            if (PlayerNumber == 2 && Input.GetKeyDown(p2ButtonKick))
            {
                hitSFX.Play();
                animator.SetTrigger("hissed");
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

        p1ButtonJump = p1inputs.ElementAt(0);
        p1ButtonLeft = p1inputs.ElementAt(1);
        p1ButtonKick = p1inputs.ElementAt(2);
        p1ButtonRight = p1inputs.ElementAt(3);

        labelp1button(p1ButtonJump, "Jump");
        labelp1button(p1ButtonLeft, "Left");
        labelp1button(p1ButtonKick, "Kick");
        labelp1button(p1ButtonRight, "Right");

        Debug.Log("shuffling p1 inputs");
    }

    public void shuffleP2Inputs()
    {
        for (int t = 0; t < p2inputs.Length; t++ )
        {
            KeyCode tmp = p2inputs[t];
            int r = Random.Range(t, p2inputs.Length);
            p2inputs[t] = p2inputs[r];
            p2inputs[r] = tmp;
        }

        p2ButtonLeft = p2inputs.ElementAt(0);
        p2ButtonRight = p2inputs.ElementAt(1);
        p2ButtonJump = p2inputs.ElementAt(2);
        p2ButtonKick = p2inputs.ElementAt(3);

        labelp2button(p2ButtonJump, "Jump");
        labelp2button(p2ButtonLeft, "Left");
        labelp2button(p2ButtonKick, "Kick");
        labelp2button(p2ButtonRight, "Right");

        Debug.Log("shuffling p2 inputs");

    }

    private void labelp1button(KeyCode p1button, string p1control)
    {
        if (p1button == KeyCode.W)
        {
            p1labels[0].text = p1control;
            Debug.Log("p1 red is " + p1control);
        }
        else if (p1button == KeyCode.A)
        {
            p1labels[1].text = p1control;
            Debug.Log("p1 yellow is " + p1control);
        }
        else if (p1button == KeyCode.S)
        {
            p1labels[2].text = p1control;
            Debug.Log("p1 green is " + p1control);
        }
        else if (p1button == KeyCode.D)
        {
            p1labels[3].text = p1control;
            Debug.Log("p1 blue is " + p1control);
        }
    }
    
    private void labelp2button(KeyCode p2button, string p2control)
    {
        if (p2button == KeyCode.UpArrow)
        {
            p2labels[0].text = p2control;
            Debug.Log("p2 red is " + p2control);
        }
        else if (p2button == KeyCode.LeftArrow)
        {
            p2labels[1].text = p2control;
            Debug.Log("p2 yellow is " + p2control);
        }
        else if (p2button == KeyCode.DownArrow)
        {
            p2labels[2].text = p2control;
            Debug.Log("p2 green is " + p2control);
        }
        else if (p2button == KeyCode.RightArrow)
        {
            p2labels[3].text = p2control;
            Debug.Log("p2 blue is " + p2control);
        }
    }
}
