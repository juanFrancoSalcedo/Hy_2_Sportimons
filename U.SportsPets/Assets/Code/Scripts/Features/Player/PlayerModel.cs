using System;

namespace U.SportsPets.Features.Player
{
    [Serializable]
    public class PlayerModel : ICopy<PlayerModel>
    {
        public float MoveSpeed = 5f;
        public float JumpForce = 8f;
        public float HitMoveSpeed = 15f;
        public float RecoveryCooldown = 2f;
        
        public bool IsGrounded;
        public bool IsAttacking;
        public bool IsRecovering;
        public float CurrentRecoveryCooldown;

        public PlayerModel Copy()
        {
            return (PlayerModel)this.MemberwiseClone();
        }
    }
}
