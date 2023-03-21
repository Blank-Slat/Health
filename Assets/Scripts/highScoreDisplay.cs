using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class highScoreDisplay : MonoBehaviour
{
    public TextMeshProUGUI highscoreDisplay;

    private void Start()
    {
        showScore();
    }
    public void showScore()
    {
        highscoreDisplay.text = "HIGHSCORE: " + PlayerPrefs.GetInt("Highscore").ToString();
    }
}
