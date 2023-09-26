using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Task.Interfaces
{
    public interface IMovement : IFlipEvent
    {
        public void SetVelocity(float horizontal, float vertical);
        public void LeftFlip();
        public void RightFlip();
    }
}
