using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : Singleton<UIController>
{
    public GameObject obj_winText;
    public Text text_score;

    public static void DisplayWinText()
    {
        Instance.obj_winText.SetActive(true);
    }

    public static void SetScore(int score)
    {
        Instance.text_score.text = "Score: " + score.ToString();
    }
}
