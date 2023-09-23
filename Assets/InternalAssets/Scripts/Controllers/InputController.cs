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
        }
    }
}
