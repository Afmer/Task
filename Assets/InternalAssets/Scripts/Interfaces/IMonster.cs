using System;
using UnityEngine;

namespace Task.Interfaces
{
    public interface IMonster
    {
        public void Chase(Transform target);
        public void StopChase();
    }
}
