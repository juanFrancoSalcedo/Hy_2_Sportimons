using UnityEngine;

namespace U.SportsPets.Features.Player
{
    public class PlayerAttack
    {
        private PlayerModel _model;
        private Rigidbody _rigidbody;
        private Transform _transform;
        private Transform _currentTarget;

        public PlayerAttack(PlayerModel model, Rigidbody rigidbody, Transform transform)
        {
            _model = model;
            _rigidbody = rigidbody;
            _transform = transform;
        }

        public void StartHitTarget(Transform target)
        {
            if (target == null)
                return;

            _currentTarget = target;
            _model.IsAttacking = true;
            
            Vector3 directionToTarget = (target.position - _transform.position).normalized;
            Vector3 hitVelocity = directionToTarget * _model.HitMoveSpeed;
            
            _rigidbody.linearVelocity = new Vector3(hitVelocity.x, _rigidbody.linearVelocity.y, hitVelocity.z);
        }

        public void EndAttack()
        {
            _model.IsAttacking = false;
            _currentTarget = null;
        }
    }
}
