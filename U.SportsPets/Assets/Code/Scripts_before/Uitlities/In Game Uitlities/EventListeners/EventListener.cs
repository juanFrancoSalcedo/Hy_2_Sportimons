using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace InGame
{
    public class EventListener : MonoBehaviour
    {
        public UnityEvent OnSelectedActions;
        public UnityEvent OnDeselectedActions;
        private ISourceSelectable selectable;

        private void Awake()
        {
            if (GetComponent<ISourceSelectable>() != null)
            {
                selectable = GetComponent<ISourceSelectable>();
            }
            else
            {
                Debug.Log("ASSIGN ISourceSelectable Script");
            }

            selectable.OnDeselected += InvokeDeselect;
            selectable.OnSelected += InvokeSelect;
        }

        private void InvokeSelect()
        {
            OnSelectedActions?.Invoke();
        }

        private void InvokeDeselect()
        {
            OnDeselectedActions?.Invoke();
        }
    }

}


