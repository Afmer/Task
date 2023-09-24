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
        public Vector2 SpawnPoint;
        private ISpawnableEntity _monsterPrefab;
        private Transform _relative;
        public Spawn(ISpawnableEntity monster, Vector2 spawnPoint, Transform relative = null)
        {
            _monsterPrefab = monster;
            SpawnPoint = spawnPoint;
            _relative = relative;
        }
        public void SpawnMonster()
        {
            var monster = _monsterPrefab.Spawn(SpawnPoint, Quaternion.Euler(0, 0, 0), _relative);
            monster.OnDead += MonsterOnDead;
        }

        private void MonsterOnDead(ISpawnableEntity obj)
        {
            _onMonsterDead?.Invoke(obj);
            OnMonsterDead?.Invoke(obj);
        }
    }
}
