using Task.Interfaces;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Task.Inventory.UI
{
    public class ItemCell : MonoBehaviour
    {
        [SerializeField]
        private Image _icon;
        [SerializeField]
        private TextMeshProUGUI _text;
        [SerializeField]
        private Button _itemButton;
        [SerializeField]
        private TextMeshProUGUI _count;
        public Button ItemButton { get { return _itemButton; } }
        private Stack _item;
        public int Index { get; private set; }
        public void SetStack(Stack stack, int index)
        {
            _item = stack;
            _icon.sprite = stack.Icon;
            _text.text = stack.ItemName;
            _count.text = stack.Count.ToString();
            Index = index;
        }
        public void Clear()
        {
            _item = null;
            _icon.sprite = null;
            _text.text = null;
            Index = -1;
            _count.text = null;
        }
    }
}
