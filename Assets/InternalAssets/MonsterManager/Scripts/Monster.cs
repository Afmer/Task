using System;
using Task.Interfaces;
using UnityEngine;
using UnityEngine.Events;

namespace Task.MonsterManager
{
    public class Monster : MonoBehaviour, IHeat, IMonster, ISpawnableEntity
    {
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

        private void Start()
        {
            OnChangeHealth += x => { return; };
            OnHeat += x => { return; };
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
    }
}
