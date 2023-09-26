using System;
using Task.Interfaces;
using UnityEngine;
using UnityEngine.Events;

namespace Task.MonsterManager
{
    public class Monster : MonoBehaviour, IHeat, IMonster, ISpawnableEntity
    {
        [SerializeField]
        private Rigidbody2D _rb;
        [SerializeField]
        private float _velocity = 20;
        [SerializeField]
        private float _health = 100;
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

        private void Start()
        {
            OnChangeHealth += x => { return; };
            OnHeat += x => { return; };
        }

        private void FixedUpdate()
        {
            if(_target != null )
            {
                var direction = (_target.position - transform.position).normalized;
                var direction2d = new Vector2(direction.x, direction.y) * _velocity * Time.deltaTime;
                _rb.MovePosition(_rb.position + direction2d);
            }
        }
        public float Health
        {
            get { return _health; }
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
    }
}
