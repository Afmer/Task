using Task.Interfaces;
using Unity.VisualScripting;
using UnityEngine;
namespace Task.MonsterManager.Drop
{
    [CreateAssetMenu(fileName = "New Drop", menuName = "Drop", order = 51)]
    public class Drop : ScriptableObject
    {
        [SerializeField]
        private GameObject[] _itemsForDrop;
        private IDropItem[] _items;
        private void OnEnable()
        {
            _items = new IDropItem[_itemsForDrop.Length];
            for (int i = 0; i < _itemsForDrop.Length; i++)
            {
                if (!(_itemsForDrop[i].TryGetComponent(out _items[i])))
                    throw new System.Exception("One of the items is not a drop item");
            }
        }
        public void InitDrop(Vector2 position)
        {
            for(int i = 0; i < _items.Length; i++)
            {
                _items[i].Spawn(position, Quaternion.Euler(0, 0, 0));
            }
        }
    }
}
