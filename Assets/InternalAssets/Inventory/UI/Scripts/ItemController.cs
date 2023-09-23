using System;
using Task.Interfaces;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Task.Inventory.UI
{
    public class ItemController : MonoBehaviour
    {
        [SerializeField]
        private Image _icon;
        [SerializeField]
        private TextMeshProUGUI _name;
        [SerializeField]
        private Button _deleteButton;
        [SerializeField]
        private Button _closeButton;
        [SerializeField]
        private UnityEvent<IItem> _onOpen;
        [SerializeField]
        private UnityEvent _onClose;
        [SerializeField]
        private UnityEvent<IItem, int> _onDelete;
        private IItem _currentItem;
        private int? _currentItemIndex;
        public event Action<IItem, int> OnDelete;
        public event Action<IItem> OnOpen;
        public event Action OnClose;
        private void Start()
        {
            OnOpen += (x) => { };
            OnClose += () => { };
            _closeButton.onClick.AddListener(Close);
            gameObject.SetActive(false);
            _deleteButton.onClick.AddListener(OnDeleteButtonClick);
        }

        private void OnDeleteButtonClick()
        {
            _onDelete.Invoke(_currentItem, _currentItemIndex.Value);
            OnDelete?.Invoke(_currentItem, _currentItemIndex.Value);
            Close();
        }

        public void Open(IItem item, int index)
        {
            _onOpen.Invoke(item);
            OnOpen(item);
            _icon.sprite = item.Sprite;
            _name.text= item.Name;
            gameObject.SetActive(true);
            _currentItem = item;
            _currentItemIndex = index;
        }
        public void Close()
        {
            _onClose.Invoke();
            OnClose();
            gameObject.SetActive(false);
            _currentItem = null;
            _currentItemIndex = null;
        }
    }
}
