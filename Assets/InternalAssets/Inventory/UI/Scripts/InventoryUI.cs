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
        private ItemCell[] _cells;
        private IInventory _inventory;
        void Start()
        {
            if(!_inventoryObject.TryGetComponent(out _inventory))
            {
                Debug.LogError("Inventory not found", this);
                throw new System.Exception("Inventory logic not found");
            }
            CloseInventory();
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
                    _cells[i].SetIcon(items[i].Sprite);
                    _cells[i].SetText(items[i].Name);
                }
            }
        }
        public void CloseInventory()
        {
            gameObject.SetActive(false);
        }
    }
}
