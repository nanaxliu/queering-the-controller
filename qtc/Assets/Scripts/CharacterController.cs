using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public int PlayerNumber;

    public float speed;
    public float jumpSpeed;
    public float attackForce;

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
            rb.AddForce( new Vector2(-speed, 0), ForceMode2D.Impulse);
        }

        if (Input.GetKey(KeyCode.D))
        {
            rb.AddForce(new Vector2(speed, 0), ForceMode2D.Impulse);
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            rb.AddForce(new Vector2(0, jumpSpeed), ForceMode2D.Impulse);
        }
    }

    void P2Movement()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            rb.AddForce(new Vector2(-speed, 0), ForceMode2D.Impulse);
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            rb.AddForce(new Vector2(speed, 0), ForceMode2D.Impulse);
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            rb.AddForce(new Vector2(0, jumpSpeed), ForceMode2D.Impulse);
        }
    }
}
