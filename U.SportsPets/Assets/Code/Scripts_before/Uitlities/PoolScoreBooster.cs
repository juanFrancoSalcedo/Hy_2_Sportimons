using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolScoreBooster : MonoBehaviour
{
    private ScoreBooster original;
    public ScoringObject scoreObject { get; set; }
    private List<ScoreBooster> boosters = new List<ScoreBooster>();

    private void Start()
    {
        scoreObject = GetComponent<ScoringObject>();
        original = GameObject.FindObjectOfType<ScoreBooster>();
    }

    public void PushBooster(Vector3 positionScore, int scoreAmount)
    {
        foreach (ScoreBooster scoreB in boosters)
        {
            if (!scoreB.gameObject.activeInHierarchy)
            {
                scoreB.gameObject.SetActive(true);
                scoreB.SetText(scoreAmount.ToString());
                scoreB.transform.position = positionScore;
                return;
            }
        }

        ScoreBooster sBoost = Instantiate(original);
        sBoost.name = "ScoreBoost" + name;
        boosters.Add(sBoost);
        sBoost.SetText(scoreAmount.ToString());
        sBoost.transform.position = positionScore;
        sBoost.textMesh.color = Constants.teamColor[scoreObject.indexScoring].colorTeam;
    }

}
