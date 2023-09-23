using System;
using System.Runtime.CompilerServices;
using Task.Inventory.Interfaces;
using UnityEngine;
using static UnityEditor.Progress;

namespace Task.Inventory
{
    public class Inventory : MonoBehaviour
    {
        [SerializeField]
        private int _inventorySize = 10;
        private IItem[] _items;
        // Start is called before the first frame update
        void Start()
        {
            _items = new IItem[_inventorySize];
            if (_inventorySize <= 0)
            {
                Debug.LogError("Inventory size error\nInventory cannot be less than or equal to zero", this);
                throw new ArgumentOutOfRangeException("Inventory size error\nInventory cannot be less than or equal to zero");
            }
        }
        public void DeleteItem(int index)
        {
            _items[index] = null;
        }
        public bool InsertItem(IItem item)
        {
            for (int i = 0; i < _items.Length; i++)
            {
                if (_items[i] == null)
                {
                    _items[i] = item;
                    return true;
                }
            }
            return false;
        }
    }
}
