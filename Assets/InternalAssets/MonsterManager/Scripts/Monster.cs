using System;
using System.Collections;
using System.Collections.Generic;
using Task.Interfaces;
using UnityEngine;
using UnityEngine.Events;

namespace Task.MonsterManager
{
    public class Monster : MonoBehaviour, IHeat
    {
        [SerializeField]
        private float _health = 100;
        [SerializeField]
        private UnityEvent<float> _onChangeHealth;
        public event Action<float> OnChangeHealth;
        [SerializeField]
        private UnityEvent<Transform> _onDead;
        public event Action<Transform> OnDead;
        private void Start()
        {
            OnChangeHealth += x => { return; };
            OnDead += x => { return; };
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
            _onDead.Invoke(transform);
            OnDead(transform);
            if(_health <= 0)
                transform.gameObject.SetActive(false);
        }
    }
}
