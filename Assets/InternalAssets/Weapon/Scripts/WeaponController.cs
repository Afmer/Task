using System.Collections;
using System.Collections.Generic;
using Task.Interfaces;
using UnityEngine;
using UnityEngine.Events;

namespace Task.Character.Weapon
{
    public class WeaponController : MonoBehaviour, IWeapon
    {
        public UnityEvent<Collider2D> OnBulletHit { get; private set; } = new();
        [SerializeField]
        private Transform _bulletSpawn;
        [SerializeField]
        private GameObject _bulletsPoolTransform;
        [SerializeField]
        private int _bulletCountInPool = 100;
        [SerializeField]
        public BulletDirectionEnum Direction;
        [SerializeField]
        private Bullet _bulletPrefab;
        [SerializeField]
        private float _rateFirePerMinute = 200;
        [SerializeField]
        private float _bulletSpeed = 20;
        private float _timeForNextShoot;
        private LinkedListNode<Bullet> _currentBullet;
        private LinkedList<Bullet> _bulletsPool = new();

        private void Start()
        {
            for(int i = 0; i < _bulletCountInPool; i++)
            {
                var bullet = Instantiate(_bulletPrefab, _bulletSpawn.transform.position, Quaternion.identity, _bulletsPoolTransform.transform);
                _bulletsPool.AddLast(bullet);
            }
            _currentBullet = _bulletsPool.First;
        }
        public void Shoot()
        {
            if (Time.time > _timeForNextShoot)
            {
                _currentBullet.Value.transform.position = _bulletSpawn.position;
                _currentBullet.Value.Launch(Direction, _bulletSpeed, OnBulletHit);
                if (_currentBullet.Next != null)
                    _currentBullet = _currentBullet.Next;
                else
                    _currentBullet = _bulletsPool.First;
                _timeForNextShoot= Time.time + (1 / (_rateFirePerMinute / 60));
            }
        }
    }
}
