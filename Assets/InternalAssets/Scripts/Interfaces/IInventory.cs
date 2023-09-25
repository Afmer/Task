using Task.Inventory;

namespace Task.Interfaces
{
    public interface IInventory
    {
        public bool PickUp();
        public IStack[] GetAll();
        public void DeleteItem(int index);
        public bool InsertItem(IItem item);
        public bool InsertItem(IItem item, int index);
    }
}
