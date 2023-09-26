using UnityEngine;
using Task.Interfaces;
using UnityEngine.Events;
using System;

namespace Task.Character
{
    public class MovementLogic : MonoBehaviour, IMovement
    {
        [SerializeField]
        private Rigidbody2D _rigidBody;
        [SerializeField]
        private float _moveSpeed = 5f;
        [SerializeField]
        private UnityEvent<string> _onFlip;
        private float _horizontal;
        private float _vertical;
        public event Action<string> OnFlip;
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
        public void LeftFlip()
        {
            transform.localScale = new Vector3(-1, 1, 1);
            _onFlip.Invoke("left");
            OnFlip?.Invoke("left");
        }
        public void RightFlip()
        {
            transform.localScale = new Vector3(1, 1, 1);
            _onFlip.Invoke("right");
            OnFlip?.Invoke("right");
        }
    }
}
