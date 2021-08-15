using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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

    public GameObject []lifes;
    public GameObject food;
    bool gotFood = false;

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

    int hits = -1;

    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        Collider2D collider = GetComponent<Collider2D>();
        playerHeight = collider.bounds.size.y;
        playerWidth = collider.bounds.size.x;
        transform.localPosition = posInicial;
        transform.localScale = sizeInicial;
        for (int i = 0; i < 3; i++) lifes[i].GetComponent<SpriteRenderer>().enabled = true;

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

        if(gotFood && transform.localPosition.y <= posInicial.y)
        {
            if (Input.anyKey)
                SceneManager.LoadScene("Winning");
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy")) {
            hits += 1;
            if(hits > 2)
            {
                SceneManager.LoadScene("GameOver");
            }
            else
            {
                lifes[2 - hits].GetComponent<SpriteRenderer>().enabled = false;
            }
        }
      if (collision.CompareTag("Food"))
      {
            food.GetComponent<SpriteRenderer>().enabled = false;
            transform.localScale = new Vector3(transform.localScale.x + 0.09f,
                                               transform.localScale.y + 0.09f,
                                               transform.localScale.z);

            gotFood = true;
            sprite.color = Color.yellow;

            for(int i = 0; i < 3; i++)
                lifes[i].GetComponent<SpriteRenderer>().enabled = true;
        }
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


}
