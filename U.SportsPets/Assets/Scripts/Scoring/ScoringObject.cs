using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoringObject : MonoBehaviour
{
    public event System.Action<int> OnScoringIncreased;
    public int indexScoring;

    //TODO Inject this
    public ScoringSystem scoreSys { get; set; }

    private void Start()
    {
        if (GameController.Instance != null)
        {
            scoreSys = GameController.Instance.leaderBoard.participantScores[indexScoring];
        }
        else
        {
            Debug.Log("THERE IS NOT GAME CONTROLLER");
        }
    }

    public void IncreseScore(int plusScore)
    {
        if (scoreSys == null)
        {
            print("THE GAME CONTROLLER BLEED");
        }
        else
        {
            scoreSys.IncreseMyScore(plusScore);
        }
    }

}
