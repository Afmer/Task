using UnityEngine;

namespace Task.Inventory.Interfaces
{
    public interface IItem
    {
        public string Name { get; }
        public Texture2D Texture { get; }
        public void PickUp();
    }
}
