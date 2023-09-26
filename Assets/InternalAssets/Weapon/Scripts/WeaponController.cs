using System.Collections;
using System.Collections.Generic;
using Task.Interfaces;
using UnityEngine;
using UnityEngine.Events;

namespace Task.Character.Weapon
{
    public class WeaponController : MonoBehaviour, IWeapon
    {
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
        [SerializeField]
        private float _damage = 5;
        [SerializeField]
        private int _clip = 30;
        [SerializeField]
        private float _reloadTime = 3;
        public int CurrentClip { get; private set; }
        private float _timeForNextShoot;
        private LinkedListNode<Bullet> _currentBullet;
        private LinkedList<Bullet> _bulletsPool = new();

        private void Start()
        {
            CurrentClip = _clip;
            for(int i = 0; i < _bulletCountInPool; i++)
            {
                var bullet = Instantiate(_bulletPrefab, _bulletSpawn.transform.position, Quaternion.identity, _bulletsPoolTransform.transform);
                _bulletsPool.AddLast(bullet);
            }
            _currentBullet = _bulletsPool.First;
        }
        public void Shoot()
        {
            if (CurrentClip <= 0)
            {
                Reload();
            }
            if (Time.time > _timeForNextShoot && CurrentClip > 0)
            {
                _currentBullet.Value.transform.position = _bulletSpawn.position;
                _currentBullet.Value.Launch(Direction, _bulletSpeed, _damage);
                if (_currentBullet.Next != null)
                    _currentBullet = _currentBullet.Next;
                else
                    _currentBullet = _bulletsPool.First;
                _timeForNextShoot= Time.time + (1 / (_rateFirePerMinute / 60));
                CurrentClip--;
            }
        }
        private IEnumerator ReloadCoroutine()
        {
            var timeForReload = Time.time + _reloadTime;
            while(true)
            {
                if (Time.time <= timeForReload)
                {
                    yield return null;
                    continue;
                }
                else
                    break;
            }
            CurrentClip = _clip;
        }
        public void Reload()
        {
            StartCoroutine(ReloadCoroutine());
        }
        public void SetLeftDirection()
        {
            Direction = BulletDirectionEnum.Left;
        }

        public void SetRightDirection()
        {
            Direction= BulletDirectionEnum.Right;
        }
    }
}
