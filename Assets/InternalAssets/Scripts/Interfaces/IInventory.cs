using Task.Inventory;

namespace Task.Interfaces
{
    public interface IInventory
    {
        public void PickUp();
        public Stack[] GetAll();
        public void DeleteItem(int index);
    }
}
