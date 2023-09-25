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
        private UnityEvent<IStackInfo> _onOpen;
        [SerializeField]
        private UnityEvent _onClose;
        [SerializeField]
        private UnityEvent<IStackInfo, int> _onDelete;
        private IStackInfo _currentItem;
        private int? _currentItemIndex;
        public event Action<IStackInfo, int> OnDelete;
        public event Action<IStackInfo> OnOpen;
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

        public void Open(IStackInfo stack, int index)
        {
            _onOpen.Invoke(stack);
            OnOpen(stack);
            _icon.sprite = stack.Icon;
            _name.text= stack.ItemName;
            gameObject.SetActive(true);
            _currentItem = stack;
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
