namespace Task.Interfaces
{
    public interface ISaveHandler
    {
        public void Delete(string saveName);
        public void Load(string saveName);
        public void Save(string saveName);
        public string SaveDirectory { get; }
    }
}
