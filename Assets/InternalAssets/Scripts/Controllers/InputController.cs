using UnityEngine;
namespace Task.Controllers
{
    public class InputController : AbstractController
    {
        private bool _isRightFlip = true;
        public override bool IsInventory()
        {
            return Input.GetKeyDown(KeyCode.I);
        }

        public override bool IsPickUp()
        {
            return Input.GetKey(KeyCode.E);
        }
        public override bool IsLeftFlip()
        {
            if (Input.GetKeyDown(KeyCode.Mouse1) && _isRightFlip)
            {
                _isRightFlip = false;
                return true;
            }
            else
                return false;
        }
        public override bool IsRightFlip()
        {
            if (Input.GetKeyDown(KeyCode.Mouse1) && !_isRightFlip)
            {
                _isRightFlip = true;
                return true;
            }
            else
                return false;
        }

        public override bool IsShoot()
        {
            return Input.GetKey(KeyCode.Mouse0);
        }

        public override Vector2 MoveController()
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");
            return new Vector2(horizontal, vertical);
        }
    }
}
