using System.Collections;
using System.Collections.Generic;
using Task.Controllers;
using UnityEngine;
using UnityEngine.UI;

namespace Task.Controller
{
    public class MobileController : AbstractController
    {
        [SerializeField]
        private FixedJoystick _movementJoystick;
        [SerializeField]
        private FixedJoystick _shooterJoystick;
        [SerializeField]
        private Button _inventoryButton;
        private bool _isInventoryButtonPressed = false;
        protected override void Init()
        {
            _inventoryButton.onClick.AddListener(OnClickInventoryButton);
        }
        private void OnClickInventoryButton()
        {
            _isInventoryButtonPressed = true;
        }
        public override bool IsInventory()
        {
            if (_isInventoryButtonPressed)
            {
                _isInventoryButtonPressed = false;
                return true;
            }
            else
                return false;
        }

        public override bool IsLeftFlip()
        {
            return _shooterJoystick.Horizontal < 0;
        }

        public override bool IsPickUp()
        {
            return false;
        }

        public override bool IsRightFlip()
        {
            return _shooterJoystick.Horizontal > 0;
        }

        public override bool IsShoot()
        {
            return _shooterJoystick.Horizontal != 0 || _shooterJoystick.Vertical != 0;
        }

        public override Vector2 MoveController()
        {
            return new Vector2(_movementJoystick.Horizontal, _movementJoystick.Vertical);
        }
    }
}
