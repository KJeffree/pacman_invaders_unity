using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOver : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] int score;

    Level level;

    void Start()
    {
        level = FindObjectOfType<Level>();
        score = level.GetEndScore();
        scoreText.text = "Final Score: " + score.ToString();
        level.RemoveTextScoreAndLives();
    }

}
