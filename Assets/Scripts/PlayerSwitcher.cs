using UnityEngine;
using UnityEngine.UI;

public class PlayerSwitcher : MonoBehaviour
{
    //Sprites
    public Sprite[] spritesWaiting; // Sequências de Sprites a serem apresentados quando personagem estiver parado
    public Sprite[] spritesMoving; //  Sequências de Sprites a serem apresentados quando personagem movimentar
    SpriteRenderer sprite; //Referência para a imagem que contém as sequências de sprites a serem apresentadas

    //Intervals
    public float interval = 0.1f; //Intervalo entre os sprites
    public float intervalMoving = 0.1f; //Intervalo de movimentação dos sprites em movimento
    
    //Curtime
    float curTime; // Referência para o tempo atual
    float curTimeMoving; // Referência para o tempo atual em movimentação

    int spriteIdx; //Indíce na array de sprites em repouso
    int spriteIdxMoving; //Indíces na array de sprites em movimentação

    bool isWaiting = true;

    //Keys
    KeyCode upKey = KeyCode.UpArrow;
    KeyCode downKey = KeyCode.DownArrow;
    KeyCode rightKey = KeyCode.RightArrow;
    KeyCode leftKey = KeyCode.LeftArrow;

    float playerHeight;
    float playerWidth;

    Vector3 posInicial = new Vector3(0, -116, -1);
    Vector3 sizeInicial = new Vector3(0.25f, 0.25f, 1);

    public float increaseStep = 1.20f;

    float sizeSky = 68;

    int hits = 0;

    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        Collider2D collider = GetComponent<Collider2D>();
        playerHeight = collider.bounds.size.y;
        playerWidth = collider.bounds.size.x;
        transform.localPosition = posInicial;
        transform.localScale = sizeInicial;
    }

    // Update is called once per frame
    void Update()
    {
        curTime += Time.deltaTime;  
        curTimeMoving += Time.deltaTime;

        if (isWaiting)
        {
            if (curTime > interval)
            {
                curTime -= interval;
                spriteIdx = (spriteIdx + 1) % spritesWaiting.Length;
                sprite.sprite = spritesWaiting[spriteIdx]; // Modifica o Sprite
            }
        }
        if (Input.GetKey(upKey) || Input.GetKey(downKey) || Input.GetKey(leftKey) || Input.GetKey(rightKey)) {
            isWaiting = false;
            if (curTimeMoving > intervalMoving) {
                curTimeMoving -= intervalMoving;
                spriteIdxMoving = (spriteIdxMoving + 1) % spritesMoving.Length;
                sprite.sprite = spritesMoving[spriteIdxMoving];
            }
        }

        if (!Input.anyKey && !isWaiting)
            sprite.sprite = spritesMoving[spritesMoving.Length-1];

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy"))
            changeSizeHit();
    }

    private void LateUpdate()
    {
        float topBound = 141f - playerHeight - sizeSky; // Barreira máxima em Y para o Player
        float bottomBound = -136f + playerHeight; // Barreira mínima em Y para o Player
        float leftBound = -80f + playerWidth;
        float rightBound = 80f - playerWidth;


        float clampedY = Mathf.Clamp(transform.localPosition.y, bottomBound, topBound);
        float clampedX = Mathf.Clamp(transform.localPosition.x, leftBound, rightBound);

        Vector3 curPos = transform.localPosition;
        transform.localPosition = new Vector3(clampedX, clampedY, curPos.z);
    }

    private void changeSizeHit()
    {
        switch(hits)
        {
            case 0:
                transform.localScale = new Vector3(transform.localScale.x * increaseStep,
                                                   transform.localScale.y * increaseStep,
                                                   transform.localScale.z * increaseStep);
                break;
            default:
                break;
        }

    }

}
