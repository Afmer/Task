using System;
using UnityEngine;

namespace Task.Interfaces
{
    public interface IMonster : IHealth
    {
        public void Chase(Transform target);
        public void StopChase();
    }
}
