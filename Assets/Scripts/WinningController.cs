using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinningController : MonoBehaviour
{
    public Text winningText;
    public Text playAgain;
    float interval = 0.5f;
    float curTime = 0f;
    string GameOver = "YOU WIN";
    string anyKey = "Press Any key to Play Again";
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
                winningText.text = winningText.text + GameOver[indice++];
            }
        }
        else // Já apresentou a string Game Over na tela
        {
            playAgain.text = anyKey;
            if (Input.anyKey)
                SceneManager.LoadScene("Menu");
        }
    }
}
