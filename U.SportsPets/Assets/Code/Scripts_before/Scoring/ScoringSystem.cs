using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoringSystem : MonoBehaviour
{
    public Board board;
    public int score;
    public TextMeshProUGUI textScore { get; set; }

    private void Start()
    {
        textScore = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
    }

    public void IncreseMyScore(int plusScore)
    {
        score += plusScore;
        board.SortScores();
        textScore.text = "" + score;
    }
}
