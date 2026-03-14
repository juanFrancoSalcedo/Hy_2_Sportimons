using B_Extensions;
using UnityEngine;
using UnityEngine.UI;
using U.SportsPets.Features.Player;

namespace U.SportsPets.Features.Player.UI
{
    public class ButtonActionJump : BaseButtonAttendant
    {
        [SerializeField] private PlayerHandler _playerHandler;

        private void Start()
        {
            if (_playerHandler == null)
            {
                Debug.LogError("[ButtonActionJump] PlayerHandler reference is missing!");
                return;
            }

            buttonComponent.onClick.AddListener(OnJumpButtonClicked);
        }

        private void OnJumpButtonClicked()
        {
            if (_playerHandler != null && _playerHandler.Model.IsGrounded)
            {
                _playerHandler.Jump();
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
