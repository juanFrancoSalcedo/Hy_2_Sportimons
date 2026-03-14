using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InGame.SwitchPricks
{
    public class AISwitchPrickManager : MonoBehaviour, IPreWarmingObject
    {
        public event System.Action<Vector3> OnDirectionCalulated;
        public Vector3 direction { get; set; }
        public Transform zeroPoint;
        private GameObject[] winObjts;
        private List<AISwitchPrick> myCompetitors = new List<AISwitchPrick>();
        private AISwitchPrick leader;

        void Start()
        {
            GameController.Instance.OnPreGameCountEnded += ActiveWarmingBehaviour;
            winObjts = GameObject.FindGameObjectsWithTag("Finish");
        }

        public void MoveAll(Vector3 orig, Vector3 obstaclePos)
        {
            OnDirectionCalulated?.Invoke(orig - obstaclePos);

        }

        public void ActiveWarmingBehaviour()
        {
          StartCoroutine(BeginLeaderBehaviour());
        }

        private IEnumerator BeginLeaderBehaviour()
        {
            while (myCompetitors.Count > 0)
            {
                yield return new WaitForSeconds(Random.Range(2f, 4.5f));
                GameObject winoCurrent = GetRandomWino();

                if (winoCurrent.activeInHierarchy)
                {
                    direction = new Vector3(winoCurrent.transform.position.x - leader.transform.position.x, 0, winoCurrent.transform.position.z - leader.transform.position.z);
                    direction = direction * 10;
                    OnDirectionCalulated?.Invoke(direction);
                }
            }
        }

        private GameObject GetRandomWino()
        {
            return winObjts[Random.Range(0, winObjts.Length)];
        }

        public void SetLeader()
        {
            if (myCompetitors.Count > 0)
            {
                leader = myCompetitors[Random.Range(0, myCompetitors.Count - 1)];
            }
        }

        public void RemoveCompetitor(AISwitchPrick competi)
        {
            myCompetitors.Remove(competi);
            SetLeader();
        }

        public void AddCompetitor(AISwitchPrick competi)
        {
            myCompetitors.Add(competi);
            SetLeader();

        }

    }
}


