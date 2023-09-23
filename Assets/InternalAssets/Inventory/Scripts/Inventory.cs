using System;
using System.Runtime.CompilerServices;
using Task.Interfaces;
using UnityEngine;
using UnityEngine.Events;

namespace Task.Inventory
{
    public class Inventory : MonoBehaviour, IInventory
    {
        [SerializeField]
        private int _inventorySize = 10;
        [SerializeField]
        private UnityEvent<IItem> _onPickUp;
        public event Action<IItem> OnPickUp;
        private IItem[] _items;
        private IItem _itemPickUp;
        // Start is called before the first frame update
        void Start()
        {
            OnPickUp += x => { return; };
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
        public void PickUp()
        {
            if (_itemPickUp != null && InsertItem(_itemPickUp))
            {
                _itemPickUp.PickUp();
                OnPickUp(_itemPickUp);
                _onPickUp.Invoke(_itemPickUp);
                _itemPickUp = null;
            }
        }
        public IItem[] GetAll()
        {
            return _items;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Item"))
            {
                IItem item;
                if (collision.gameObject.TryGetComponent(out item))
                {
                    _itemPickUp = item;
                }
            }
        }
        private void OnTriggerExit2D(Collider2D collision)
        {
            _itemPickUp = null;
        }
    }
}
