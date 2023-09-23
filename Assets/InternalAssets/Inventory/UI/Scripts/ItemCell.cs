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
        public void SetIcon(Sprite sprite)
        {
            _icon.sprite = sprite;
        }
        public void SetText(string text)
        {
            _text.text = text;
        }
    }
}
