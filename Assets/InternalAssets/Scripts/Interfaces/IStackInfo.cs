using UnityEngine;

namespace Task.Interfaces
{
    public interface IStackInfo
    {
        public bool IsEmpty();
        public Sprite Icon { get; }
        public string ItemName { get; }
        public int Count { get; }
        public bool IsFreeSpace();
        public int ItemID { get; }
    }
}
