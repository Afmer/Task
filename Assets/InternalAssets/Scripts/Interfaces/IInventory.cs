namespace Task.Interfaces
{
    public interface IInventory
    {
        public void PickUp();
        public IItem[] GetAll();
        public void DeleteItem(int index);
    }
}
