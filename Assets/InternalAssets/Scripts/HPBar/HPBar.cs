using System.Collections;
using System.Collections.Generic;
using Task.Interfaces;
using UnityEngine;
using UnityEngine.UI;

namespace HPBar
{
    public class HPBar : MonoBehaviour
    {
        [SerializeField]
        private Image _bar;
        [SerializeField]
        private float _maxHP;
        [SerializeField]
        private GameObject _object;
        private IHealth _healthObject;
        private void Awake()
        {
            if(!_object.TryGetComponent(out _healthObject))
            {
                Debug.LogError("Health object not found", this);
                throw new System.Exception("Health object not found");
            }
        }

        private void FixedUpdate()
        {
            _bar.fillAmount =  _healthObject.Health / _maxHP;
        }
    }
}
