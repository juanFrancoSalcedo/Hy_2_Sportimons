using UnityEngine;

namespace U.SportsPets.Features.Player
{
    public class PlayerRecovery
    {
        private PlayerModel _model;

        public PlayerRecovery(PlayerModel model)
        {
            _model = model;
        }

        public void StartRecovery()
        {
            if (_model.CurrentRecoveryCooldown <= 0 && !_model.IsRecovering)
            {
                _model.IsRecovering = true;
                _model.CurrentRecoveryCooldown = _model.RecoveryCooldown;
            }
        }

        public void UpdateRecovery(float deltaTime)
        {
            if (_model.IsRecovering)
            {
                _model.CurrentRecoveryCooldown -= deltaTime;
                
                if (_model.CurrentRecoveryCooldown <= 0)
                {
                    _model.IsRecovering = false;
                    _model.CurrentRecoveryCooldown = 0;
                }
            }
        }

        public bool CanRecover()
        {
            return _model.CurrentRecoveryCooldown <= 0 && !_model.IsRecovering;
        }
    }
}
