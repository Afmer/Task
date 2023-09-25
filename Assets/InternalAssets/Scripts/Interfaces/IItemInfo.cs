using UnityEngine;
namespace Task.Interfaces
{
    public interface IItemInfo
    {
        public string Name { get; }
        public int Id { get; }
        public Sprite Sprite { get; }
    }
}
