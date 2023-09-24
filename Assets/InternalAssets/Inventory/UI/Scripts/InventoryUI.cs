using System;
using Task.Interfaces;
using UnityEngine;
namespace Task.Inventory.UI
{
    public class InventoryUI : MonoBehaviour, IInventoryUI
    {
        [SerializeField]
        private GameObject _inventoryObject;
        [SerializeField]
        private Transform _cellsContainer;
        [SerializeField]
        private ItemCell _cellPrefab;
        [SerializeField]
        private ItemController _itemController;
        private ItemCell[] _cells;
        private IInventory _inventory;
        private void Error(string message)
        {
            Debug.LogError(message, this);
            throw new System.Exception(message);
        }
        void Start()
        {
            if (_inventoryObject == null)
                Error("Inventory object is null");
            if (_cellsContainer == null)
                Error("Cells container is null");
            if (_cellPrefab == null)
                Error("Cell prefab is null");
            if (_itemController == null)
                Error("Item controller is null");
            if(!_inventoryObject.TryGetComponent(out _inventory))
            {
                Debug.LogError("Inventory not found", this);
                throw new System.Exception("Inventory logic not found");
            }
            _itemController.OnDelete += OnItemDelete;
            CloseInventory();
        }

        private void OnItemDelete(Stack item, int index)
        {
            _inventory.DeleteItem(index);
            _cells[index].Clear();
        }

        public void OpenInventory()
        {
            gameObject.SetActive(true);
            var items = _inventory.GetAll();
            if(_cells == null)
            {
                _cells = new ItemCell[items.Length];
                for(int i = 0; i < items.Length; i++)
                {
                    _cells[i] = Instantiate(_cellPrefab, _cellsContainer);
                }
            }
            for(int i = 0; i < items.Length;i++)
            {
                if (items[i] != null)
                {
                    var localStack = items[i];
                    var localIndex = i;
                    _cells[i].SetStack(items[i], i);
                    _cells[i].ItemButton.onClick.AddListener(() =>
                    {
                        _itemController.Open(localStack, localIndex);
                    });
                }
            }
        }
        public void CloseInventory()
        {
            gameObject.SetActive(false);
        }
    }
}
