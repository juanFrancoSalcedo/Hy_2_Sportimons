using B_Extensions;
using UnityEngine;
using UnityEngine.UI;
using U.SportsPets.Features.Player;

namespace U.SportsPets.Features.Player.UI
{
    public class ButtonActionRecovery : BaseButtonAttendant
    {
        [SerializeField] private PlayerHandler _playerHandler;

        private void Start()
        {
            if (_playerHandler == null)
            {
                Debug.LogError("[ButtonActionRecovery] PlayerHandler reference is missing!");
                return;
            }

            buttonComponent.onClick.AddListener(OnRecoveryButtonClicked);
        }

        private void Update()
        {
            if (_playerHandler != null)
            {
                buttonComponent.interactable = _playerHandler.CanRecover();
            }
        }

        private void OnRecoveryButtonClicked()
        {
            if (_playerHandler != null && _playerHandler.CanRecover())
            {
                _playerHandler.Recovery();
            }
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
