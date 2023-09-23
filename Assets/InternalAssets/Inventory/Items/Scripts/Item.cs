using System.Collections;
using System.Collections.Generic;
using Task.Inventory.Interfaces;
using UnityEngine;
namespace Task.Inventory.Item
{
    public class Item : MonoBehaviour, IItem
    {
        [SerializeField]
        private string _name;
        public string Name => _name;
    }
}
