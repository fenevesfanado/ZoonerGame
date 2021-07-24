
using UnityEngine;

public class Enemy1_Controller : MonoBehaviour
{
    //Constants
    private const float POS_X_START = -49f;
    private const float POS_Y_START = -68f;
    private const float INITIAL_SPEED = 20;    
    public const int RIGHT_BORDER = 50;
    public const int LEFT_BORDER = -50;
    public float test = 0;

    private float speed;
    Vector2 direction = Vector2.right; // Starts walking to the right
    Vector2 invert = new Vector2(-1,0);
    Rigidbody2D rb;    
    SpriteRenderer sp;

    // Start is called before the first frame update
    void Start()
    {
        transform.localPosition = new Vector3(POS_X_START, POS_Y_START, -1);
        rb = GetComponent<Rigidbody2D>();
        sp = GetComponent<SpriteRenderer>();
        speed = INITIAL_SPEED;
    }
       

    // Update is called once per frame
    void Update()
    {
        if(transform.localPosition.x == RIGHT_BORDER) { 
            sp.flipX = !sp.flipX;
            rb.velocity *= invert;
            direction = Vector2.left;
            transform.localPosition = new Vector3 (transform.localPosition.x - 1f,
                                                   transform.localPosition.y,
                                                   transform.localPosition.z);
            return;
        }
        if (transform.localPosition.x == LEFT_BORDER)
        {
            sp.flipX = !sp.flipX;
            rb.velocity *= invert;
            direction = Vector2.right;
            transform.localPosition = new Vector3(transform.localPosition.x + 1f,
                                                   transform.localPosition.y,
                                                   transform.localPosition.z);
            return;
        }

        speed += 0.1f;
        rb.velocity = direction * speed;       
    }

    void LateUpdate()
    {
        float ClampedX = Mathf.Clamp(transform.localPosition.x, LEFT_BORDER, RIGHT_BORDER);
        transform.localPosition = new Vector3(ClampedX, transform.localPosition.y, transform.localPosition.z);
        
    }
}
