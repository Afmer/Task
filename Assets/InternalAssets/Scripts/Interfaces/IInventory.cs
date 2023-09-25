using Task.Inventory;

namespace Task.Interfaces
{
    public interface IInventory
    {
        public void PickUp();
        public IStack[] GetAll();
        public void DeleteItem(int index);
    }
}
