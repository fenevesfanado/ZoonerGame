using UnityEngine;
using UnityEngine.SceneManagement; // Necess�rio para carregar cenas

public class Menu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKey)
            SceneManager.LoadScene("Game");
    }
}
