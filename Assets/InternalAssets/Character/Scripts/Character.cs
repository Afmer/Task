using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Task.Character
{
    public class Character : MonoBehaviour
    {
        [SerializeField]
        private Collider2D _hitBox;
        [SerializeField]
        private int _health = 100;
        public int Health
        {
            get { return _health; }
        }
    }
}
