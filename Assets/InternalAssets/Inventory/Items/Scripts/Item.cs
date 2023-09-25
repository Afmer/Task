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
        [SerializeField]
        private int _id;
        public string Name => _name;

        public Sprite Sprite => _sprite;

        public int Id => _id;

        public void Delete()
        {
            Destroy(gameObject);
        }

        public void PickUp()
        {
            gameObject.SetActive(false);
        }
        public IDropItem Spawn(Vector2 position, Quaternion rotation, Transform relative)
        {
            return Instantiate(this, position, rotation, relative);
        }
    }
}
