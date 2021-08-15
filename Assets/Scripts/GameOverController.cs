using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverController : MonoBehaviour
{
    public Text textGameOver;
    public Text tryAgain;
    float interval = 0.5f;
    float curTime = 0f;
    string GameOver = "Game Over";
    string anyKey = "Press Any Key to try again";
    int indice = 0;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (indice < GameOver.Length)
        {
            curTime += Time.deltaTime;
            if (curTime > interval)
            {
                curTime = 0;
                textGameOver.text = textGameOver.text + GameOver[indice++];
            }
        }
        else // Já apresentou a string Game Over na tela
        {
            tryAgain.text = anyKey;
            if (Input.anyKey)
                SceneManager.LoadScene("Game");
        }
    }
}
