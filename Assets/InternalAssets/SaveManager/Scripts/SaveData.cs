using System;
using Task.SaveManager.Inventory;

namespace Task.SaveManager
{
    [Serializable]
    public class SaveData
    {
        public InventoryData Inventory = new();
    }
}
