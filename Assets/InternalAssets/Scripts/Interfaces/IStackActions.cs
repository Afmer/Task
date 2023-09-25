namespace Task.Interfaces
{
    public interface IStackActions
    {
        public IItem Pop();
        public void Push(IItem item);
        public void Clear();
    }
}
