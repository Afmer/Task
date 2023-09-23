using System.Collections;
using System.Collections.Generic;
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
        public void OnChangeHealth(float healt)
        {
            _bar.fillAmount = healt / _maxHP;
        }
    }
}
