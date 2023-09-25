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
        private IStack[] _items;
        private IItem _itemPickUp;
        // Start is called before the first frame update
        void Start()
        {
            OnPickUp += x => { return; };
            _items = new Stack[_inventorySize];
            if (_inventorySize <= 0)
            {
                Debug.LogError("Inventory size error\nInventory cannot be less than or equal to zero", this);
                throw new ArgumentOutOfRangeException("Inventory size error\nInventory cannot be less than or equal to zero");
            }
        }
        public void DeleteItem(int index)
        {
            var stack = _items[index];
            if (stack != null)
            {
                if (!stack.IsEmpty())
                {
                    var item = stack.Pop();
                    item.Delete();
                    if (stack.IsEmpty())
                        _items[index] = null;
                }
                else
                    _items[index] = null;
            }
            else
                throw new Exception("Item not found");
        }
        public bool InsertItem(IItem item)
        {
            int firstEmptyCellIndex = -1;
            for (int i = 0; i < _items.Length; i++)
            {
                bool isCanPush = _items[i] != null && _items[i].ItemID == item.Id && _items[i].IsFreeSpace();
                if (isCanPush)
                {
                    _items[i].Push(item);
                    return true;
                }
                else if(firstEmptyCellIndex == -1 && _items[i] == null)
                    firstEmptyCellIndex = i;
            }
            if (firstEmptyCellIndex >= 0)
            {
                _items[firstEmptyCellIndex] = new Stack(item);
                return true;
            }
            else
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
        public IStack[] GetAll()
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
