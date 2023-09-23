using Task.Interfaces;
using UnityEditor.VersionControl;
using UnityEngine;
namespace Task.Controllers
{
    public class InputController : MonoBehaviour
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
        private bool isInventoryOpen = false;
        void Start()
        {
            if (!_character.TryGetComponent(out _movement))
            {
                Debug.LogError("Movement logic not found", this);
                throw new System.Exception("Movement logic not found");
            }
            if(!_weaponObject.TryGetComponent(out _weapon))
            {
                Debug.LogError("Weapon not found", this);
                throw new System.Exception("Weapon not found");
            }
            if(!_inventoryObject.TryGetComponent(out _inventory))
            {
                Debug.LogError("Inventory not found", this);
                throw new System.Exception("Inventory not found");
            }
            if(!_inventoryUIControllerObject.TryGetComponent(out _inventoryUIController))
            {
                Debug.LogError("InventoryUIController not found", this);
                throw new System.Exception("InventoryUIController not found");
            }
        }
        void Update()
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");
            _movement.SetVelocity(horizontal, vertical);
            if(Input.GetKey(KeyCode.Mouse0))
            {
                _weapon.Shoot();
            }
            if(Input.GetKey(KeyCode.E))
            {
                _inventory.PickUp();
            }
            if(Input.GetKeyDown(KeyCode.I))
            {
                if (isInventoryOpen)
                {
                    _inventoryUIController.CloseInventory();
                    isInventoryOpen = false;
                }
                else
                {
                    _inventoryUIController.OpenInventory();
                    isInventoryOpen = true;
                }
            }
        }
    }
}
