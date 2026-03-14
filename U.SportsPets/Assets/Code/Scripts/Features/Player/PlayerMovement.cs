using UnityEngine;

namespace U.SportsPets.Features.Player
{
    public class PlayerMovement
    {
        private PlayerModel _model;
        private Rigidbody _rigidbody;
        private Transform _transform;

        public PlayerMovement(PlayerModel model, Rigidbody rigidbody, Transform transform)
        {
            _model = model;
            _rigidbody = rigidbody;
            _transform = transform;
        }

        public void MoveForward()
        {
            Vector3 forwardVelocity = _transform.forward * _model.MoveSpeed;
            _rigidbody.linearVelocity = new Vector3(forwardVelocity.x, _rigidbody.linearVelocity.y, forwardVelocity.z);
        }

        public void Jump()
        {
            if (_model.IsGrounded)
            {
                _rigidbody.AddForce(Vector3.up * _model.JumpForce, ForceMode.Impulse);
            }
        }
    }
}
