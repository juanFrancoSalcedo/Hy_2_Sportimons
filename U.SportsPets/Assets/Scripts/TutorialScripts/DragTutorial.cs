using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragTutorial : MonoBehaviour, ITutoriable
{
    [SerializeField] private DraggableObject dragObj;
    [SerializeField] LevelLoader loader;
    [SerializeField] Animator _animator;
    [SerializeField] int levelReal;
    [SerializeField] GameObject[] disabledObjets;
    private float timeDragging;
    public int countMissions { get; set; }
    [SerializeField] int limitMission = 1;
    public DirectionDragTuto dragTutoDirect;
    public enum DirectionDragTuto
    {
        Everything,
        Horizontal
    }

    private void Start()
    {
        if (dragTutoDirect == DirectionDragTuto.Horizontal)
        {
            _animator.SetBool("Horizontal",true);
        }
        dragObj.OnDragging += AddTimeDragging;
    }

    void AddTimeDragging(Vector3 dragginPos)
    {
        timeDragging += Time.deltaTime;

        if(timeDragging > 2)
        {
            _animator.enabled = false;
            foreach (GameObject gamObj in disabledObjets)
            {
                gamObj.SetActive(false);
            }
        }
    }

    public void PlusMission()
    {
        countMissions++;
        if (countMissions > limitMission)
        {
            loader.LoadSpecificSceneTransition(levelReal);
        }
    }
}
