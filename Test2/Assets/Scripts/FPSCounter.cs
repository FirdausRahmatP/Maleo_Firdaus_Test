using UnityEngine;
using UnityEngine.UI;

public class FPSCounter : MonoBehaviour
{
    public int avgFrameRate;
    private Text text;
    private void Start()
    {
        text = GetComponent<Text>();
    }
    public void Update()
    {
        float current = Time.frameCount / Time.time;
        avgFrameRate = (int)current;
        text.text = avgFrameRate.ToString();
    }
}