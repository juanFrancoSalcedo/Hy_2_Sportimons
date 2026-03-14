using UnityEngine;

namespace U.SportsPets.Features.Player
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerHandler : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private Transform _groundCheck;
        [SerializeField] private LayerMask _groundLayer;
        
        [Header("Settings")]
        [SerializeField] private float _groundCheckRadius = 0.3f;
        
        private PlayerModel _model;
        private PlayerMovement _movement;
        private PlayerAttack _attack;
        private PlayerRecovery _recovery;

        public PlayerModel Model => _model;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _model = new PlayerModel();
            InitializeComponents();
        }

        private void InitializeComponents()
        {
            _movement = new PlayerMovement(_model, _rigidbody, transform);
            _attack = new PlayerAttack(_model, _rigidbody, transform);
            _recovery = new PlayerRecovery(_model);
        }

        private void Update()
        {
            CheckGroundStatus();
            _recovery.UpdateRecovery(Time.deltaTime);
        }

        private void FixedUpdate()
        {
            if (!_model.IsAttacking)
            {
                _movement.MoveForward();
            }
        }

        private void CheckGroundStatus()
        {
            if (_groundCheck != null)
            {
                _model.IsGrounded = Physics.CheckSphere(_groundCheck.position, _groundCheckRadius, _groundLayer);
            }
        }

        public void Jump() => _movement.Jump();

        public void HitTarget(Transform target) => _attack.StartHitTarget(target);

        public void EndHit() => _attack.EndAttack();

        public void Recovery() => _recovery.StartRecovery();

        public bool CanRecover() => _recovery.CanRecover();

        public bool IsRecovering() => _model.IsRecovering;


#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            if (_groundCheck != null)
            {
                Gizmos.color = Color.yellow;
                Gizmos.DrawWireSphere(_groundCheck.position, _groundCheckRadius);
            }
        }
#endif
    }
}
