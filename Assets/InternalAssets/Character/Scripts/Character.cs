using System;
using System.Collections;
using System.Collections.Generic;
using Task.Interfaces;
using UnityEngine;
using UnityEngine.Events;

namespace Task.Character
{
    public class Character : MonoBehaviour, ICharacter
    {
        [SerializeField]
        private UnityEvent<ICharacter> _onHeat;
        [SerializeField]
        private UnityEvent<ICharacter> _onDead;
        public event Action<ICharacter> OnHeat;
        public event Action<ICharacter> OnDead;
        [SerializeField]
        private float _health = 100;
        public float Health
        {
            get { return _health; }
        }

        public void Heat(float damage)
        {
            OnHeat?.Invoke(this);
            _onHeat.Invoke(this);
            _health -= damage;
            if(_health <= 0)
            {
                OnDead?.Invoke(this);
                _onDead?.Invoke(this);
                gameObject.SetActive(false);
            }
        }
    }
}
