using UnityEngine;

namespace Task.Interfaces
{
    public interface IItem
    {
        public string Name { get; }
        public int Id { get; }
        public Sprite Sprite { get; }
        public void PickUp();
        public void Delete();
    }
}
