using Task.Interfaces;
using UnityEngine;
namespace Task.Inventory.Item
{
    public class Item : MonoBehaviour, IItem
    {
        [SerializeField]
        private string _name;
        [SerializeField]
        private Sprite _sprite;
        public string Name => _name;

        public Texture2D Texture => _sprite.texture;

        public void PickUp()
        {
            gameObject.SetActive(false);
        }

        private void Start()
        {
        }
    }
}
