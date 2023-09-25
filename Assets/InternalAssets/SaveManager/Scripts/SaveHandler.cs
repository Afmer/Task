using System;
using System.IO;
using Task.Interfaces;
using Task.SaveManager.Inventory;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Task.SaveManager
{
    public class SaveHandler : MonoBehaviour, ISaveHandler
    {
        [SerializeField]
        private string _directorySaves = "/Saves/";
        [SerializeField]
        private GameObject _inventoryObject;
        [SerializeField]
        private ScriptableObject _itemIdsObject;
        private IItemIds _itemIds;
        private IInventory _inventory;
        void Start()
        {
            if(!(_inventoryObject.TryGetComponent(out _inventory)))
            {
                Debug.LogError("Inventory not found", this);
                throw new Exception("Inventory not found");
            }
            _itemIds = _itemIdsObject as IItemIds;
            if(_itemIds == null)
            {
                Debug.LogError("Item ids not found", this);
                throw new Exception("Item ids not found");
            }
        }
        public void Save(string saveName)
        {
            var data = new SaveData();
            data.Inventory = GetInventoryData();
            string json = JsonUtility.ToJson(data);
            var datapath = GetSavePath(saveName);
            if (!Directory.Exists(SaveDirectory))
                Directory.CreateDirectory(SaveDirectory);
            File.WriteAllText(datapath, json);
        }
        private string GetSavePath(string saveName) => SaveDirectory + saveName + ".json";
        public string SaveDirectory => Application.dataPath + _directorySaves;

        private InventoryData GetInventoryData()
        {
            var data = new InventoryData();
            IStackInfo[] items = _inventory.GetAll();
            data.Stacks = new StackData[items.Length];
            for (int i = 0; i < items.Length; i++)
            {
                var stackData = new StackData();
                if (items[i] == null)
                    stackData.Count = 0;
                else
                {
                    stackData.Count = items[i].Count;
                    stackData.ItemId = items[i].ItemID;
                }
                data.Stacks[i] = stackData;
            }
            return data;
        }
        public void Load(string saveName)
        {
            string json = File.ReadAllText(GetSavePath(saveName));
            var data = JsonUtility.FromJson<SaveData>(json);
            LoadInventory(data);
        }
        private void LoadInventory(SaveData data)
        {
            var stacksData = data.Inventory.Stacks;
            IStackActions[] stacks = _inventory.GetAll();
            if(stacksData.Length != stacks.Length)
            {
                Debug.LogError("Inventory size error", this);
                throw new Exception("Inventory size error");
            }
            _inventory.Clear();
            for(int i = 0; i < stacks.Length; i++)
            {
                if (stacksData[i].Count == 0)
                    stacks[i] = null;
                else
                {
                    IItem item;
                    for(int j = 0; j < stacksData[i].Count; j++)
                    {
                        item = (IItem)(_itemIds.GetItem(stacksData[i].ItemId).Spawn(Vector2.zero, Quaternion.Euler(0, 0, 0)));
                        item.PickUp();
                        _inventory.InsertItem(item, i);
                    }
                }
            }
        }

    }
}
