using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Task.Interfaces;
using UnityEngine;
using UnityEngine.Events;

namespace Task.Character.Weapon
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField]
        private int _launchTimeSeconds;
        [SerializeField]
        private Rigidbody2D _rigidBody;
        private float _endLaunchTime;
        private Vector3? _strengthVector;
        private float _damage;
        void Start()
        {
            transform.gameObject.SetActive(false);
        }

        // Update is called once per frame
        void Update()
        {
            if(_strengthVector != null)
            {
                var tempMove = _strengthVector.Value * Time.deltaTime;
                _rigidBody.MovePosition(_rigidBody.position + new Vector2(tempMove.x, tempMove.y));
                if(Time.time > _endLaunchTime)
                {
                    _strengthVector = null;
                    transform.gameObject.SetActive(false);
                    _endLaunchTime = 0;
                    _damage = 0;
                }
            }
        }
        public void Launch(BulletDirectionEnum direction, float speed, float damage)
        {
            var currentTime = Time.time;
            _endLaunchTime = currentTime + _launchTimeSeconds;
            switch(direction)
            {
                case BulletDirectionEnum.Left:
                    _strengthVector = new Vector3(-1 * speed, 0, 0);
                    break;
                case BulletDirectionEnum.Right:
                    _strengthVector = new Vector3(speed, 0, 0);
                    break;
                case BulletDirectionEnum.Up:
                    _strengthVector = new Vector3(0, speed, 0);
                    break;
                case BulletDirectionEnum.Down:
                    _strengthVector = new Vector3(0, -1 * speed, 0);
                    break;
                default:
                    Debug.LogError("Direction error", this);
                    throw new System.Exception("Direction error");
            }
            transform.gameObject.SetActive(true);
            _damage = damage;
        }
        public void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.CompareTag("Hitbox"))
            {
                Hitbox hitbox;
                if(collision.gameObject.TryGetComponent(out hitbox))
                {
                    hitbox.Heat(_damage);
                }
            }
        }
    }
}
