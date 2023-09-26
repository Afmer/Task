using Task.Interfaces;
using UnityEngine;
namespace Task.Controllers
{
    public abstract class AbstractController : MonoBehaviour
    {
        [SerializeField]
        private GameObject _character;
        [SerializeField]
        private GameObject _weaponObject;
        [SerializeField]
        private GameObject _inventoryObject;
        [SerializeField]
        private GameObject _inventoryUIControllerObject;
        private IInventoryUI _inventoryUIController;
        private IInventory _inventory;
        private IMovement _movement;
        private IWeapon _weapon;
        void Start()
        {
            if (!_character.TryGetComponent(out _movement))
            {
                Debug.LogError("Movement logic not found", this);
                throw new System.Exception("Movement logic not found");
            }
            if (!_weaponObject.TryGetComponent(out _weapon))
            {
                Debug.LogError("Weapon not found", this);
                throw new System.Exception("Weapon not found");
            }
            if (!_inventoryObject.TryGetComponent(out _inventory))
            {
                Debug.LogError("Inventory not found", this);
                throw new System.Exception("Inventory not found");
            }
            if (!_inventoryUIControllerObject.TryGetComponent(out _inventoryUIController))
            {
                Debug.LogError("InventoryUIController not found", this);
                throw new System.Exception("InventoryUIController not found");
            }
        }
        private void Update()
        {
            var vectorVelocity = MoveController();
            _movement.SetVelocity(vectorVelocity.x, vectorVelocity.y);
            if (IsShoot())
                _weapon.Shoot();
            if (IsPickUp())
                _inventory.PickUp();
            if (IsInventory())
            {
                _inventoryUIController.OpenCloseInventory();
            }
            if(IsLeftFlip())
            {
                _movement.LeftFlip();
            }
            else if (IsRightFlip())
            {
                _movement.RightFlip();
            }
        }
        public abstract Vector2 MoveController();
        public abstract bool IsShoot();
        public abstract bool IsPickUp();
        public abstract bool IsInventory();
        public abstract bool IsLeftFlip();
        public abstract bool IsRightFlip();
    }
}
