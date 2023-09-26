using System;
using System.Collections;
using System.Collections.Generic;
using Task.Interfaces;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Task
{
    public class PlayerDeathHandler : MonoBehaviour
    {
        [SerializeField]
        private GameObject _saveHandlerObject;
        private ISaveHandler _saveHandler;
        private void Awake()
        {
            if(!_saveHandlerObject.TryGetComponent(out _saveHandler))
            {
                Debug.LogError("Save handler not found", this);
                throw new Exception("Save handler not found");
            }
        }

        public void OnDeath()
        {
            _saveHandler.Delete("default");
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(currentSceneIndex);
        }
    }
}
