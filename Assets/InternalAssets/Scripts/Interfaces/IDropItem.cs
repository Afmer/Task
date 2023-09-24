using UnityEngine;

namespace Task.Interfaces
{
    public interface IDropItem
    {
        public IDropItem Spawn(Vector2 position, Quaternion rotation, Transform relative = null);
    }
}
