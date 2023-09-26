using System.Collections;
using System.Collections.Generic;
using Task.Interfaces;
using TMPro;
using UnityEngine;
namespace Task.InfoUI
{
    public class InfoUIManager : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI _weaponClipTextMesh;
        [SerializeField]
        private GameObject _weaponClipObject;
        private IWeaponClip _weaponClip;
        private void Awake()
        {
            if(!_weaponClipObject.TryGetComponent(out _weaponClip))
            {
                Debug.LogError("Weapon clip not found", this);
                throw new System.Exception("Weapon clip not found");
            }
        }
        void FixedUpdate()
        {
            _weaponClipTextMesh.text = _weaponClip.CurrentClip.ToString();
        }
    }
}
