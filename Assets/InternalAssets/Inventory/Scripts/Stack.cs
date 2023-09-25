using System.Collections.Generic;
using Task.Interfaces;
using UnityEngine;

namespace Task.Inventory
{
    public class Stack : IStack
    {
        private Stack<IItem> _items = new();
        public int MaxSize { get; private set; }
        public int Count => _items.Count;
        public int ItemID { get; private set; }
        public Sprite Icon { get; private set; }
        public string ItemName { get; private set; }
        public Stack(IItem item, int size = 64)
        {
            if(size <= 0)
                throw new System.ArgumentException("Stack size should not be less than 1");
            _items.Push(item);
            ItemID = item.Id;
            Icon = item.Sprite;
            ItemName = item.Name;
            MaxSize = size;
        }
        public void Push(IItem item)
        {
            if (item.Id != ItemID)
                throw new System.ArgumentException("Insertion into a stack of a type different from the stack");
            if (!IsFreeSpace())
                throw new System.Exception("No free space");
            _items.Push(item);
        }
        public IItem Pop()
        {
            if(IsEmpty())
                throw new System.Exception("Stack is empty");
            return _items.Pop();
        }
        public bool IsFreeSpace()
        {
            return _items.Count < MaxSize;
        }
        public bool IsEmpty()
        {
            return _items.Count == 0;
        }
    }
}
