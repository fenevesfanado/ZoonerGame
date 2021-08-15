using UnityEngine;

public class PlayerController : MonoBehaviour
{

    // Start is called before the first frame update
    public SpriteRenderer sp;
    Rigidbody2D rb;
    public float speed;
    float curTime;
    public float interval = 0.1f;
    bool left = false;
    bool right = true;
    

    KeyCode upKey = KeyCode.UpArrow;
    KeyCode downKey = KeyCode.DownArrow;
    KeyCode rightKey = KeyCode.RightArrow;
    KeyCode leftKey = KeyCode.LeftArrow;


    void Start(){
        rb = GetComponent<Rigidbody2D>();
        sp = GetComponent<SpriteRenderer>();
        sp.enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            transform.localPosition = new Vector3(transform.localPosition.x,
                                                  transform.localPosition.y - 10.0f,
                                                  transform.localPosition.z);
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKey(upKey))
            rb.velocity = Vector2.up * speed;
        else if (Input.GetKey(downKey))
            rb.velocity = Vector2.down * speed;
        else if (Input.GetKey(rightKey))
        {
            sp.flipX = right; // Inverte o sentido do personagem
            rb.velocity = Vector2.right * speed; // Movimenta o personagem para direita
        }
        else if (Input.GetKey(leftKey))
        {
            sp.flipX = left; // Inverte o sentido do personagem
            rb.velocity = Vector2.left * speed; // Acelera o personagem para esquerda

        }
        else
            rb.velocity = Vector2.zero;

    }

}
