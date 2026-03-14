using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class SwipeTutorial : MonoBehaviour, ITutoriable
    {
        [SerializeField] SwipeManager managerSwipe;
        [SerializeField] Animator _animator;
        [SerializeField] int limitSwipe;
        [SerializeField] LevelLoader loader;
        [SerializeField] int levelReal;
        [SerializeField] GameObject[] objectsDisableCompleted;
        public int countMissions { get; set; }
        int countSwipe = 0;
        [SerializeField] bool swipeCompleteMission;

        void Start()
        {
            managerSwipe.OnSwipedMagnitud += PlusSwipe;
        }


        void PlusSwipe(Vector3 dir)
        {
            countSwipe++;

            if (limitSwipe > countSwipe)
            {
                _animator.SetBool("Anim" + countSwipe, true);
            }
            else
            {
                foreach (GameObject gamo in objectsDisableCompleted)
                {
                    gamo.SetActive(false);
                }
                _animator.enabled = false;
            }

            if (swipeCompleteMission)
            {
                PlusMission();
            }
        }

        public void PlusMission()
        {
            countMissions++;
            if (countMissions >= limitSwipe)
            {
                loader.LoadSpecificSceneTransition(levelReal);
                managerSwipe.OnSwipedMagnitud -= PlusSwipe;
            }
        }
    }



