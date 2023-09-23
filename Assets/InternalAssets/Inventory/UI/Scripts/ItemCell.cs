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
        public Button ItemButton { get { return _itemButton; } }
        private IItem _item;
        public int Index { get; private set; }
        public void SetItem(IItem item, int index)
        {
            _item = item;
            _icon.sprite = item.Sprite;
            _text.text = item.Name;
            Index = index;
        }
        public void Clear()
        {
            _item = null;
            _icon.sprite = null;
            _text.text = null;
            Index = -1;
        }
    }
}
