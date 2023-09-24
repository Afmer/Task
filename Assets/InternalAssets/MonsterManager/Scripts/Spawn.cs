using System;
using Task.Interfaces;
using UnityEngine;

namespace Task.MonsterManager
{
    public class Spawn
    {
        [SerializeField]
        private event Action<ISpawnableEntity> _onMonsterDead;
        public event Action<ISpawnableEntity> OnMonsterDead;
        private ISpawnableEntity _monsterPrefab;
        private Transform _spawnPoint;
        public Spawn(ISpawnableEntity monster, Transform spawnPoint)
        {
            _monsterPrefab = monster;
            _spawnPoint = spawnPoint;
        }
        public void SpawnMonster()
        {
            var monster = _monsterPrefab.Spawn(_spawnPoint.position, _spawnPoint.rotation, _spawnPoint);
            monster.OnDead += MonsterOnDead;
        }

        private void MonsterOnDead(ISpawnableEntity obj)
        {
            _onMonsterDead?.Invoke(obj);
            OnMonsterDead?.Invoke(obj);
        }
    }
}
