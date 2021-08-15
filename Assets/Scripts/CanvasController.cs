using UnityEngine.UI;
using UnityEngine;


public class CanvasController : MonoBehaviour
{

    public Text time;
    float timeCalculator;

    // Start is called before the first frame update
    void Start()
    {
        timeCalculator = 0;
    }

    // Update is called once per frame
    void Update()
    {
        timeCalculator += Time.deltaTime;
        time.text = string.Format("{0:00}", Time.time - timeCalculator);
    }
}
