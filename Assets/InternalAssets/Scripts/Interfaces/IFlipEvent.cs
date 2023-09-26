using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Task.Interfaces
{
    public interface IFlipEvent
    {
        public event Action<string> OnFlip;
    }
}
