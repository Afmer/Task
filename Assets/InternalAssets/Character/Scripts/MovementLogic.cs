using UnityEngine;
using Task.Interfaces;

namespace Task.Character
{
    public class MovementLogic : MonoBehaviour, IMovement
    {
        [SerializeField]
        private Rigidbody2D _rigidBody;
        [SerializeField]
        private float _moveSpeed = 5f;
        private float _horizontal;
        private float _vertical;
        public void SetVelocity(float horizontal, float vertical)
        {
            _horizontal = horizontal;
            _vertical = vertical;
        }
        void FixedUpdate()
        {
            Vector2 movement = new Vector2(_horizontal, _vertical) * _moveSpeed * Time.deltaTime;
            _rigidBody.MovePosition(_rigidBody.position + movement);
        }
    }
}
