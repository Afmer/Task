using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Task.Interfaces;
using Task.Inventory;
using UnityEngine;
namespace Task.SaveManager
{
    public class SaveManager : MonoBehaviour
    {
        [SerializeField]
        private GameObject _saveHandlerObject;
        private ISaveHandler _saveHandler;
        private void Awake()
        {
            if(!(_saveHandlerObject.TryGetComponent(out _saveHandler)))
            {
                Debug.LogError("Save handler not found", this);
                throw new Exception("Save handler not found");
            }
        }

        private void Start()
        {
            string savePath = _saveHandler.SaveDirectory + "default.json";
            if (File.Exists(savePath))
                _saveHandler.Load("default");
        }

        private void OnApplicationQuit()
        {
            string saveDirectory = _saveHandler.SaveDirectory;
            if(!Directory.Exists(saveDirectory))
                Directory.CreateDirectory(saveDirectory);
            _saveHandler.Save("default");
        }
    }
}
