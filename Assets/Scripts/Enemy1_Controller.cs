
using UnityEngine;

public class Enemy1_Controller : MonoBehaviour
{
    //Constants
    private const float POS_X_START = -49f;
    private const float POS_Y_START = -68f;
    private const float INITIAL_SPEED = 10;
    public const int RIGHT_BORDER = 50;
    public const int LEFT_BORDER = -50;
    private bool LEFT_DIRECTION = false;
    private bool RIGHT_DIRECTION = true; 
    public const int TIME_STATE_CHANGE = 4;
    public Transform playerPosition;


    private float speed;
    Vector2 direction = Vector2.right; // Starts walking to the right
    Vector2 invert = new Vector2(-1,0); //Used to invert moviment
    Rigidbody2D rb; //RigidBody reference
    SpriteRenderer sp; //Sprite Renderer reference
    


    // Start is called before the first frame update
    void Start()
    {
        transform.localPosition = new Vector3(POS_X_START, POS_Y_START, -1); //Initial position
        rb = GetComponent<Rigidbody2D>();
        sp = GetComponent<SpriteRenderer>();
        speed = INITIAL_SPEED;
        sp.enabled = true; //Makes the enemy visible
    }


    // Update is called once per frame
    void Update()
    {
        if (Mathf.Abs((playerPosition.localPosition.y - transform.localPosition.y)) < 20 )
        {
            float nDirectionX = (playerPosition.localPosition.x - transform.localPosition.x) / 30 ;
            float nDirectionY = (playerPosition.localPosition.y - transform.localPosition.y) / 30;
            direction = new Vector2(nDirectionX, nDirectionY);
            if(playerPosition.localPosition.x < transform.localPosition.x && RIGHT_DIRECTION)
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

    void LateUpdate()
    {
        float ClampedX = Mathf.Clamp(transform.localPosition.x, LEFT_BORDER, RIGHT_BORDER);
        transform.localPosition = new Vector3(ClampedX, transform.localPosition.y, transform.localPosition.z);
        
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
}
