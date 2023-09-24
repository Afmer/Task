using Task.Interfaces;
using UnityEngine;
namespace Task.MonsterManager
{
    public class MonsterManager : MonoBehaviour
    {
        [SerializeField]
        private Transform[] _spawnsTransform;
        [SerializeField]
        private GameObject[] _monstersObjects;
        [SerializeField]
        private Drop.Drop[] _monstersDrop;
        private Spawn[] _spawns;
        void Start()
        {
            if (_monstersObjects.Length != _spawnsTransform.Length || _monstersObjects.Length != _monstersDrop.Length)
                throw new System.Exception("Mismatch between spawn points and monsters or between drop and monsters");
            ISpawnableEntity[] monsters = new ISpawnableEntity[_monstersObjects.Length];
            for(int i = 0; i < _monstersObjects.Length; i++)
            {
                if (!(_monstersObjects[i].TryGetComponent(out monsters[i])))
                    throw new System.Exception("One of the monsters is not a spawnable entity");
            }
            _spawns = new Spawn[_spawnsTransform.Length];
            for(int i = 0; i < _spawns.Length; i++)
            {
                var localDrop = _monstersDrop[i];
                _spawns[i] = new Spawn(monsters[i], _spawnsTransform[i]);
                _spawns[i].OnMonsterDead += x =>
                {
                    localDrop.InitDrop(x.Position);
                };
                _spawns[i].SpawnMonster();
            }
        }
    }
}
