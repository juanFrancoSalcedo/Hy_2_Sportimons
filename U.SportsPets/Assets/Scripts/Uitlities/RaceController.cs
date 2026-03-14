using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InGame
{
    public class RaceController : MonoBehaviour, IFeastable, IKillable
    {
        [SerializeField] MetersLava.LavaCompetitor player;
        public TriggerDetector triggerGoal { get; private set; }
        public RaceCompetitor[] competitorsRace;
        public SportCreature[] competitorFeatures;
        [SerializeField] private bool useLifes;
        public RaceTrack[] tracks;
        private bool arrived;
        private bool lost;

        private void Awake()
        {
            competitorsRace = new RaceCompetitor[6];
            SetTrack();
        }
        
        void SetTrack()
        {
            foreach (RaceTrack _track in tracks) { _track.trackOperative.gameObject.SetActive(false); }
            RaceTrack currenTrack = tracks[Random.Range(0, tracks.Length)];
            currenTrack.trackOperative.gameObject.SetActive(true);
            player.enviromentTranslator = currenTrack.trackOperative;
            triggerGoal = currenTrack.goalTrack;
            triggerGoal.OnTriggerEntered += AddTime;
        }

        public void AddTime(Transform other)
        {
            if (arrived) { return; }
            GameController.Instance.timer.stopTimer = true;
            arrived = true;
            competitorsRace[0].scoreSys = GameController.Instance.leaderBoard.participantScores[0];
            if (lost) { competitorsRace[0].timeRace = Mathf.Infinity; }
            else { competitorsRace[0].timeRace = GameController.Instance.timer.initTime; }
            for (int i = 1; i < competitorsRace.Length; i++)
            {
                competitorsRace[i].scoreSys = GameController.Instance.leaderBoard.participantScores[i];
                competitorsRace[i].TimeSimulated(competitorFeatures[i], useLifes);
            }
            SortAndGivePoint();
        }

        public void LoseAddTime()
        {
            lost = true;
            AddTime(transform);
        }

        private void SortAndGivePoint()
        {
            for (int i = 0; i < competitorsRace.Length - 1; i++)
            {
                for (int j = 0; j < competitorsRace.Length - 1; j++)
                {
                    if (competitorsRace[j].timeRace > competitorsRace[j + 1].timeRace)
                    {
                        RaceCompetitor buffer = competitorsRace[j + 1];
                        competitorsRace[j + 1] = competitorsRace[j];
                        competitorsRace[j] = buffer;
                    }
                }
            }

            int summation = 10;

            for (int i = 0; i < competitorsRace.Length; i++)
            {
                if (competitorsRace[i].timeRace < 1000)
                {
                    competitorsRace[i].scoreSys.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = "" + competitorsRace[i].timeRace.ToString("0.000");
                    Celebrate();
                }
                else
                {
                    competitorsRace[i].scoreSys.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = "Out";
                    Dead();
                }

                competitorsRace[i].scoreSys.score = summation;
                summation--;
            }
            GameController.Instance.leaderBoard.SortScores();
            GameController.Instance.Invoke("EndMatch", 0.9f);
        }

        //   [System.Serializable]
        public struct RaceCompetitor
        {
            public ScoringSystem scoreSys;
            public float timeRace;

            public void TimeSimulated(SportCreature competitor, bool canLifes)
            {
                float disasvantage = (float)(competitor.force + competitor.speed) / 10;
                float handicap = (float)competitor.speed / 10;
                float rayoTime = (float)(46 - (2 * competitor.speed)) + Random.Range(2, 12f);
                rayoTime += CalculateLifesRest(canLifes);
                timeRace = rayoTime;
            }

            private int CalculateLifesRest(bool _useLifes)
            {
                if (!_useLifes)
                {
                    return 0;
                }

                float randomFell = Random.Range(0f, 10f);
                int plusTime = 0;

                if (randomFell >= 2 && randomFell <= 3)
                {
                    plusTime = 1;
                    //Debug.Log(1+" , "+scoreSys.name);
                }
                else if (randomFell >= 1 && randomFell <= 2)
                {
                    plusTime = 2;
                    //Debug.Log(2 + " , " + scoreSys.name);
                }
                else if (randomFell >= 0 && randomFell <= 1)
                {
                    plusTime = 1000;
                    //Debug.Log(1000 + " , " + scoreSys.name);
                }

                return plusTime;
            }
        }
        [System.Serializable]
        public struct RaceTrack
        {
            public TranslateObject trackOperative;
            public TriggerDetector goalTrack;

        }

        public void Celebrate()
        {
           player.animatorControl.transform.rotation = Quaternion.Euler(0, 180, 0);
           player.animatorControl.SetAnimationCharacter("Celebration", true);
        }

        public void Dead()
        {
            player.animatorControl.transform.rotation = Quaternion.Euler(0, 180, 0);
            player.animatorControl.SetAnimationCharacter("Dead", true);
        }
    }

}

