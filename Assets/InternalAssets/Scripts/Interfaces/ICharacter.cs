using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Task.Interfaces
{
    public interface ICharacter : IHeat, IHealth
    {
        public event Action<ICharacter> OnDead;
        public event Action<ICharacter> OnHeat;
    }
}
