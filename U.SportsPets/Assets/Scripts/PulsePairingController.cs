using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace InGame.Pulse
{
    public class PulsePairingController : PairingController, IEndMatchAffiliable
    {
        public PulsePlayer[] pulsePlayers;
        //private PulsePlayer exep1;
        //private PulsePlayer exep2;
        float timeSumMatch1;
        float timeSumMatch2;
        bool inMatching1End;
        bool inMatching2End;
        private PulsePlayer play1Match1;
        private PulsePlayer play2Match1;
        private PulsePlayer play1Match2;
        private PulsePlayer play2Match2;
        bool firstEnter = true;


        private void Awake()
        {
            Debug.Log("make sure the scoringObjects are in the same order as the pulsePlayers.");
            //   PairingPulsePlayersExeption(pulsePlayers[0], pulsePlayers[1]);

            if (GameController.Instance != null)
            {
                GameController.Instance.OnMatchEnded += EndMatchDelegate;
            }
        }

        private void Update()
        {
            if (inMatching1End)
            {
                StopCoroutine(CompeteMatch1(null, null, 0, 0));
                DistribuiteScors(play1Match1,play1Match2,1);
                inMatching1End = false;
            }

            if (inMatching2End)
            {
                StopCoroutine(CompeteMatch2(null, null, 0, 0));
                DistribuiteScors(play1Match2, play2Match2,2);
                inMatching2End = false;
            }
        }

        public void PairingPulsePlayersExeption(PulsePlayer exeption1, PulsePlayer exeption2)
        {
            if (!firstEnter)
            {
                if (ReferenceEquals(exeption2, play1Match1) || ReferenceEquals(exeption2, play2Match1)){inMatching1End = true;}
                if (ReferenceEquals(exeption2, play1Match2) || ReferenceEquals(exeption2, play2Match2)){inMatching2End = true;}
            }
            firstEnter = false;
            
            List<PulsePlayer> aros = new List<PulsePlayer>();
            List<int> randoList = new List<int>();

            foreach (PulsePlayer player in pulsePlayers)
            {
                if (!ReferenceEquals(player, exeption1) && !ReferenceEquals(player, exeption2))
                {
                    aros.Add(player);
                }
            }

            for (int ropa = 0; ropa < 4; ropa++)
            {
                bool comisario = false;

                while (!comisario)
                {
                    int rando = Random.Range(0, 4);

                    if (!randoList.Contains(rando))
                    {
                        randoList.Add(rando);
                        comisario = true;
                    }
                }
            }

            play1Match1 = aros[0];
            play2Match1 = aros[1];
            play1Match2 = aros[2];
            play2Match2 = aros[3];

            float baseTimeOneLess = CalculateTime(aros[randoList[0]].athlete, aros[randoList[1]].athlete,true);
            float baseTimeOnePlus = CalculateTime(aros[randoList[0]].athlete, aros[randoList[1]].athlete,false);
            float baseTimeTwoLess = CalculateTime(aros[randoList[2]].athlete, aros[randoList[3]].athlete,true);
            float baseTimeTwoPlus = CalculateTime(aros[randoList[2]].athlete, aros[randoList[3]].athlete,false);
            //print(play1Match1.name +" Vs "+play2Match1);
            //print(play1Match2.name +" Vs "+play2Match2);
            StartCoroutine(CompeteMatch1(aros[0], aros[1], baseTimeOneLess,baseTimeOnePlus+10));
            StartCoroutine(CompeteMatch2(aros[2], aros[3], baseTimeTwoLess,baseTimeTwoPlus+10));
            CancelInvoke("TimingMatch1");
            CancelInvoke("TimingMatch2");
            TimingMatch1();
            TimingMatch2();
        }

        public float CalculateTime(SportCreature participant1, SportCreature participant2, bool plus)
        {
            float dif1=   (float)(participant1.force + participant1.spirit)/5;
            float dif2  = (float)(participant2.force + participant2.spirit)/5;
            float place =  10-Mathf.Sqrt(Mathf.Pow((dif1-dif2),2));
            return place;
        }

        public IEnumerator CompeteMatch1(PulsePlayer p1, PulsePlayer p2, float minTime, float maxTime)
        {
            yield return new WaitForSeconds(Random.Range(minTime, maxTime));
            DistribuiteScors(p1,p2,1);
        }

        public IEnumerator CompeteMatch2(PulsePlayer p1, PulsePlayer p2, float minTime, float maxTime)
        {
            yield return new WaitForSeconds(Random.Range(minTime, maxTime));
            DistribuiteScors(p1, p2,2);
        }

        private void DistribuiteScors(PulsePlayer _p1, PulsePlayer _p2, int matchId)
        {
            float percent1 = (float)((_p1.athlete.force + _p1.athlete.spirit) / 2);
            float percent2 = (float)((_p2.athlete.force + _p2.athlete.spirit) / 2);
            float cienPercent = (float)(percent1 + percent2);

            float sp = Random.Range(percent1 / cienPercent, percent2 / cienPercent);

            if (sp > 0.1f)
            {
                _p1.scoreObject.IncreseScore((int)timeSumMatch1);

            }
            else if (sp < -0.1f)
            {
                _p2.scoreObject.IncreseScore((int)timeSumMatch2);
            }
            else
            {
                print("Empate");
                _p1.scoreObject.IncreseScore(7);
                _p2.scoreObject.IncreseScore(7);
            }

            if (matchId == 1)
            {
                timeSumMatch1 = 0;
            }
          
            if (matchId == 2)
            {
                timeSumMatch2 = 0;
            }
        }

        void TimingMatch1()
        {
            timeSumMatch1++;
            Invoke("TimingMatch1",0.9f);   
        }

        void TimingMatch2()
        {
            timeSumMatch2++;
            Invoke("TimingMatch2",0.9f);
        }

        public void EndMatchDelegate()
        {
            this.enabled = false;
        }
    }
}


