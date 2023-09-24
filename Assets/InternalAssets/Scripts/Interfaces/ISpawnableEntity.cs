using System;
using UnityEngine;
namespace Task.Interfaces
{
    public interface ISpawnableEntity
    {
        public ISpawnableEntity Spawn(Vector2 position, Quaternion rotation, Transform relative = null);
        public event Action<ISpawnableEntity> OnDead;
        public Vector2 Position { get; }
    }
}
