using B_Extensions;
using UnityEngine;
using UnityEngine.UI;
using U.SportsPets.Features.Player;

namespace U.SportsPets.Features.Player.UI
{
    public class ButtonActionHit : BaseButtonAttendant
    {
        [SerializeField] private PlayerHandler _playerHandler;
        [SerializeField] private Transform _targetTransform;

        private void Start()
        {
            if (_playerHandler == null)
            {
                Debug.LogError("[ButtonActionHit] PlayerHandler reference is missing!");
                return;
            }

            buttonComponent.onClick.AddListener(OnHitButtonClicked);
        }

        private void OnHitButtonClicked()
        {
            if (_playerHandler != null && _targetTransform != null)
            {
                _playerHandler.HitTarget(_targetTransform);
            }
        }

        public void SetTarget(Transform target)
        {
            _targetTransform = target;
        }

        public void EnableButton()
        {
            buttonComponent.interactable = true;
        }

        public void DisableButton()
        {
            buttonComponent.interactable = false;
        }

        public void InjectPlayer(PlayerHandler playerHandler)
        {
            _playerHandler = playerHandler;
        }
    }
}
