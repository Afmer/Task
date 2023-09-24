using Task.Interfaces;
using UnityEditor.VersionControl;
using UnityEngine;
namespace Task.Controllers
{
    public class InputController : AbstractController
    {
        public override bool IsInventory()
        {
            return Input.GetKeyDown(KeyCode.I);
        }

        public override bool IsPickUp()
        {
            return Input.GetKey(KeyCode.E);
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
