using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

    public class GameController : MonoBehaviour
    {
        public static GameController Instance;
        [SerializeField] private TextMeshProUGUI textTimer;
        public Timer timer;
        public event System.Action OnPreGameCountEnded;
        public Board leaderBoard;
        [Header("~~~~~~~~ End Game~~~~~~")]
        [SerializeField] Animator canvasEndMatch;
        [SerializeField] TextMeshProUGUI textPlace;
        [SerializeField] GameObject[] disableObjects;
        public System.Action OnMatchEnded;
        private int countLoser;

        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                timer.OnCalculatedTimeString += ShowTime;
                timer.enabled = false;
            }
            else
            {
                Destroy(gameObject);
            }
            
            foreach (GameObject disObj in disableObjects)
            {
                disObj.GetComponent<IPerishable>().OnMyGameEnded += IncreaseLoser;
            }
        }

        private void IncreaseLoser()
        {
            countLoser++;
            if (countLoser >= disableObjects.Length)
            {
                EndMatch();
            }
        }

        private void EndOfCount()
        {
            OnPreGameCountEnded?.Invoke();
            timer.enabled = true;
        }

        private void ShowTime(string timeStr, float timeFlt)
        {
            textTimer.text = timeStr;
            if (timeFlt < 0)
            {
                EndMatch();
            }
        }

        public IEnumerator ResurrectObject(float timeResurresct, GameObject objectResurrect)
        {
            yield return new WaitForSeconds(timeResurresct);
            objectResurrect.SetActive(true);
        }

        public void EndMatch()
        {
            timer.stopTimer = true;
            Camera.main.SendMessage("ZoomOut");
            OnMatchEnded?.Invoke();
            Invoke("ShowCanvasEnd", 0.9f);
        }

        private void ShowCanvasEnd()
        {
            canvasEndMatch.gameObject.SetActive(true);
            int IDOL = GameObject.Find("Place").transform.GetSiblingIndex() + 1;
            textPlace.text = "" + IDOL;

            switch (IDOL)
            {
                case 1:
                    canvasEndMatch.SetBool("Gold",true);
                break;
                case 2:
                    canvasEndMatch.SetBool("Silver", true);
                break;
                case 3:
                    canvasEndMatch.SetBool("Bronze", true);
                break;
                case 4:
                    canvasEndMatch.SetBool("NoMedal", true);
                break;
                case 5:
                    canvasEndMatch.SetBool("NoMedal", true);
                break;
                case 6:
                    canvasEndMatch.SetBool("Last", true);
                break;

            }
        }

        public int GetEarnedMoney()
        {
            int IDOL = GameObject.Find("Place").transform.GetSiblingIndex() + 1;
            int monet = 0;

            switch (IDOL)
            {
                case 1:monet = 30; break;
                case 2:monet = 20; break;
                case 3:monet = 15; break;
                case 4:monet = 10; break;
                case 5: monet = 5; break;
                case 6: monet = 0; break;
            }
            return monet;
        }
    }



