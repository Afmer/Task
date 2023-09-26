using System.Collections;
using System.Collections.Generic;
using Task.Interfaces;
using UnityEngine;
namespace Task.Character
{
    public class FlipWeaponHandler : MonoBehaviour
    {
        [SerializeField]
        private GameObject _weaponObject;
        private IWeaponDirection _weapon;
        [SerializeField]
        private GameObject _playerObject;
        private IFlipEvent _player;
        private void Awake()
        {
            if(!_weaponObject.TryGetComponent(out _weapon))
            {
                Debug.LogError("Weapon not found", this);
                throw new System.Exception("Weapon not found");
            }
            if(!_playerObject.TryGetComponent(out _player))
            {
                Debug.LogError("Player not found", this);
                throw new System.Exception("Player not found");
            }
        }
        private void Start()
        {
            _player.OnFlip += _player_OnFlip;
        }

        private void _player_OnFlip(string direction)
        {
            if (direction == "left")
                _weapon.SetLeftDirection();
            else if (direction == "right")
                _weapon.SetRightDirection();
        }
    }
}
