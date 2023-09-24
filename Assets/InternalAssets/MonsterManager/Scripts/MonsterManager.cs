using System.Collections;
using Task.Interfaces;
using UnityEngine;
namespace Task.MonsterManager
{
    public class MonsterManager : MonoBehaviour
    {
        [SerializeField]
        private GameObject[] _monstersObjects;
        [SerializeField]
        private Drop.Drop[] _monstersDrop;
        [SerializeField]
        private Transform _startRectPoint;
        [SerializeField]
        private Transform _endRectPoint;
        private Spawn[] _spawns;
        private float _minX;
        private float _maxX;
        private float _minY;
        private float _maxY;
        private int _killedMonsters = 0;
        void Start()
        {
            if (_monstersObjects.Length != _monstersDrop.Length)
                throw new System.Exception("Mismatch between spawn points and monsters or between drop and monsters");
            ISpawnableEntity[] monsters = new ISpawnableEntity[_monstersObjects.Length];
            for(int i = 0; i < _monstersObjects.Length; i++)
            {
                if (!(_monstersObjects[i].TryGetComponent(out monsters[i])))
                    throw new System.Exception("One of the monsters is not a spawnable entity");
            }
            _spawns = new Spawn[_monstersObjects.Length];
            _minX = _startRectPoint.transform.position.x > _endRectPoint.transform.position.x 
                ? _endRectPoint.transform.position.x : _startRectPoint.transform.position.x;
            _maxX = _startRectPoint.transform.position.x < _endRectPoint.transform.position.x
                ? _endRectPoint.transform.position.x : _startRectPoint.transform.position.x;
            _minY = _startRectPoint.transform.position.y > _endRectPoint.transform.position.y
                ? _endRectPoint.transform.position.y : _startRectPoint.transform.position.y;
            _maxY = _startRectPoint.transform.position.y < _endRectPoint.transform.position.y
                ? _endRectPoint.transform.position.y : _startRectPoint.transform.position.y;
            for (int i = 0; i < _spawns.Length; i++)
            {
                var localDrop = _monstersDrop[i];
                var randX = Random.Range(_minX, _maxX);
                var randY = Random.Range(_minY, _maxY);
                _spawns[i] = new Spawn(monsters[i], new Vector2(randX, randY));
                _spawns[i].OnMonsterDead += x =>
                {
                    _killedMonsters++;
                    if(_killedMonsters >= _monstersObjects.Length)
                    {
                        _killedMonsters = 0;
                        StartCoroutine(Respawn(5));
                        Respawn(5);
                    }
                    localDrop.InitDrop(x.Position);
                };
                _spawns[i].SpawnMonster();
            }
        }

        public IEnumerator Respawn(float time)
        {
            float endTime = Time.time + time;
            while (true)
            {
                if(Time.time < endTime)
                {
                    yield return null;
                    continue;
                }
                foreach (var spawn in _spawns)
                {
                    var randX = Random.Range(_minX, _maxX);
                    var randY = Random.Range(_minY, _maxY);
                    spawn.SpawnPoint = new Vector2(randX, randY);
                    spawn.SpawnMonster();
                }
                break;
            }
        }
    }
}
