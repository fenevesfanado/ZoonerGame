using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fly_Controller : MonoBehaviour
{
    private const float POS_X_START = -47f; //Starts X Position
    private const float POS_Y_START = 68f; //Starts Y Position
    private const float POS_Z_START = -1;
    private const float INITIAL_SPEED = 10;
    public const int RIGHT_BORDER = 50;
    public const int LEFT_BORDER = -50;
    private bool LEFT_DIRECTION = false;
    private bool RIGHT_DIRECTION = true;
    public Transform playerPosition;


    private float speed;
    Vector2 direction = Vector2.right; // Starts walking to the right
    Vector2 invert = new Vector2(-1, 0); //Used to invert moviment
    Rigidbody2D rb; //RigidBody reference
    SpriteRenderer sp; //Sprite Renderer reference

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
