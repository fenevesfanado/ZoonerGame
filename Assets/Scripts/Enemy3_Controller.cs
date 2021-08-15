using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy3_Controller : MonoBehaviour
{

    //Constants
    private const float POS_X_START = 0f;
    private const float POS_Y_START = 3f;
    private const float POS_Z_START = -1;
    private const float INITIAL_SPEED = 10;
    public const int RIGHT_BORDER = 50;
    public const int LEFT_BORDER = -50;
    private bool LEFT_DIRECTION = false;
    private bool RIGHT_DIRECTION = true;
    public Transform playerPosition;

    private float speed; // Velocity
    Vector2 direction = Vector2.right; // Starts walking to the left
    Vector2 invert = new Vector2(-1, 0);
    Rigidbody2D rb;
    SpriteRenderer sp;

    // Start is called before the first frame update
    void Start()
    {
        transform.localPosition = new Vector3(POS_X_START, POS_Y_START, POS_Z_START);
        rb = GetComponent<Rigidbody2D>();
        sp = GetComponent<SpriteRenderer>();
        speed = INITIAL_SPEED;
        sp.enabled = true;
    }


    // Update is called once per frame
    void Update()
    {
        if (Mathf.Abs((playerPosition.localPosition.y - transform.localPosition.y)) < 20)
        {
            float nDirectionX = (playerPosition.localPosition.x - transform.localPosition.x) / 30;
            float nDirectionY = (playerPosition.localPosition.y - transform.localPosition.y) / 30;
            direction = new Vector2(nDirectionX, nDirectionY);
            if (playerPosition.localPosition.x < transform.localPosition.x && RIGHT_DIRECTION)
            {
                sp.flipX = !sp.flipX;
                RIGHT_DIRECTION = false;
                LEFT_DIRECTION = true;
            }
            else if (playerPosition.localPosition.x > transform.localPosition.x && LEFT_DIRECTION)
            {
                sp.flipX = !sp.flipX;
                RIGHT_DIRECTION = true;
                LEFT_DIRECTION = false;
            }

            speed += 0.03f;
        }
        else if (direction != Vector2.right && direction != Vector2.left)
        {
            if (RIGHT_DIRECTION)
                direction = Vector2.right;
            else if (LEFT_DIRECTION)
                direction = Vector2.left;
        }

        rb.velocity = direction * speed;
        checkBorders();
    }


    private void checkBorders()
    {
        if (transform.localPosition.x >= RIGHT_BORDER)
        {
            sp.flipX = !sp.flipX;
            RIGHT_DIRECTION = false;
            LEFT_DIRECTION = true;
            rb.velocity *= invert;
            direction = Vector2.left;
            transform.localPosition = new Vector3(transform.localPosition.x - 1f,
                                                   transform.localPosition.y,
                                                   transform.localPosition.z);
            return;
        }
        if (transform.localPosition.x <= LEFT_BORDER)
        {
            sp.flipX = !sp.flipX;
            RIGHT_DIRECTION = true;
            LEFT_DIRECTION = false;
            rb.velocity *= invert;
            direction = Vector2.right;
            transform.localPosition = new Vector3(transform.localPosition.x + 1f,
                                                   transform.localPosition.y,
                                                   transform.localPosition.z);
            return;
        }
    }

    void LateUpdate()
    {
        float ClampedX = Mathf.Clamp(transform.localPosition.x, LEFT_BORDER, RIGHT_BORDER);
        transform.localPosition = new Vector3(ClampedX, transform.localPosition.y, transform.localPosition.z);

    }
}
