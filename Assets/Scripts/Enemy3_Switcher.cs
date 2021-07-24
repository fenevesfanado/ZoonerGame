using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy3_Switcher : MonoBehaviour
{
    // Start is called before the first frame update

    SpriteRenderer sprite;
    public Sprite[] sprites;
    float curTime;
    int spriteIdx;
    public float interval = 0.1f;
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

        curTime += Time.deltaTime;
        
        if (curTime > interval)
            {
                curTime -= interval;
                spriteIdx = (spriteIdx + 1) % sprites.Length;
                sprite.sprite = sprites[spriteIdx]; // Modifica o Sprite
            }

    }
}
