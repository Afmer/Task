using System;
using System.Collections;
using Task.Interfaces;
using UnityEngine;
using UnityEngine.Events;

namespace Task.MonsterManager
{
    public class Monster : MonoBehaviour, IHeat, IMonster, ISpawnableEntity
    {
        [SerializeField]
        private Collider2D _collider;
        [SerializeField]
        private Rigidbody2D _rb;
        [SerializeField]
        private float _velocity = 20;
        [SerializeField]
        private float _health = 100;
        public float Health => _health;
        [SerializeField]
        private UnityEvent<float> _onChangeHealth;
        public event Action<float> OnChangeHealth;
        [SerializeField]
        private UnityEvent<ISpawnableEntity> _onHeat;
        [SerializeField]
        private UnityEvent<ISpawnableEntity> _onDead;
        public event Action<ISpawnableEntity> OnHeat;
        public event Action<ISpawnableEntity> OnDead;
        public Vector2 Position => new Vector2(transform.position.x, transform.position.y);
        private Transform _target;
        private bool _isRetreat = false;

        private void Start()
        {
            OnChangeHealth += x => { return; };
            OnHeat += x => { return; };
        }

        private void FixedUpdate()
        {
            if(_target != null && !_isRetreat)
            {
                var directionVector = (_target.position - transform.position);
                float length = directionVector.magnitude;
                if(length < 0.5)
                {
                    IHeat player;
                    if (_target.gameObject.TryGetComponent(out player))
                    {
                        player.Heat(10);
                        StartCoroutine(RetreatCoroutine());
                        return;
                    }
                }
                var direction = directionVector.normalized;
                var direction2d = new Vector2(direction.x, direction.y) * _velocity * Time.deltaTime;
                _rb.MovePosition(_rb.position + direction2d);

            }
        }
        public void Heat(float damage)
        {
            _health -= damage;
            _onChangeHealth.Invoke(_health);
            OnChangeHealth(_health);
            _onHeat.Invoke(this);
            OnHeat(this);
            if (_health <= 0)
                Kill();
        }
        public void Kill()
        {
            OnDead?.Invoke(this);
            _onDead?.Invoke(this);
            transform.gameObject.SetActive(false);
        }
        public ISpawnableEntity Spawn(Vector2 position, Quaternion rotation, Transform relative = null)
        {
            ISpawnableEntity result;
            if (relative != null)
                result = Instantiate(this, position, rotation, relative);
            else
                result = Instantiate(this, position, rotation);
            return result;
        }
        public void Chase(Transform target)
        {
            _target = target;
        }
        public void StopChase()
        {
            _target = null;
        }
        private IEnumerator RetreatCoroutine()
        {
            _isRetreat = true;
            float retrationTime = 1;
            float retrationEndTime = Time.time + retrationTime;
            while(true)
            {
                var direction = (_target.position - transform.position).normalized * -1;
                var direction2d = new Vector2(direction.x, direction.y) * _velocity * Time.deltaTime;
                _rb.MovePosition(_rb.position + direction2d);
                if (Time.time > retrationEndTime)
                    break;
                else
                    yield return null;
            }
            _isRetreat = false;
        }
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.collider.CompareTag("Monster"))
                Physics2D.IgnoreCollision(collision.collider, _collider);
        }
    }
}
