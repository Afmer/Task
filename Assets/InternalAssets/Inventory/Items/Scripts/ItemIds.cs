using System.Collections.Generic;
using Task.Interfaces;
using UnityEngine;
namespace Task.SaveManager
{
    [CreateAssetMenu(fileName = "New ItemIds", menuName = "Inventory/ItemIds", order = 51)]
    public class ItemIds : ScriptableObject, IItemIds
    {
        [SerializeField]
        private GameObject[] _itemObjects;
        private IItem[] _items;
        private Dictionary<int, IItem> _idsForItems;
        private void OnEnable()
        {
            if (_items == null)
            {
                _items = new IItem[_itemObjects.Length];
                for (int i = 0; i < _itemObjects.Length; i++)
                {
                    if(_itemObjects[i] == null)
                    {
                        _items = null;
                        _idsForItems = null;
                        return;
                    }
                    if (!(_itemObjects[i].TryGetComponent(out _items[i])))
                    {
                        Debug.LogError("Item error", this);
                        throw new System.Exception("Item error");
                    }
                }
            }
            if(_idsForItems == null)
            {
                _idsForItems = new();
                foreach(var item in _items)
                    _idsForItems.Add(item.Id, item);
            }
        }
        public IItem GetItem(int id)
        {
            return _idsForItems[id];
        }
    }
}
