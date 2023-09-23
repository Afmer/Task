using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;

namespace Task.Character.Weapon
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField]
        private int _launchTimeSeconds;
        private float _endLaunchTime;
        private Vector3? _strengthVector;
        private UnityEvent<Collider2D> _hitEvent;
        void Start()
        {
            transform.gameObject.SetActive(false);
        }

        // Update is called once per frame
        void Update()
        {
            if(_strengthVector != null)
            {
                transform.Translate(_strengthVector.Value * Time.deltaTime, Space.World);
                if(Time.time > _endLaunchTime)
                {
                    _strengthVector = null;
                    transform.gameObject.SetActive(false);
                    _endLaunchTime = 0;
                    _hitEvent = null;
                }
            }
        }
        public void Launch(BulletDirectionEnum direction, float speed, UnityEvent<Collider2D> hitEvent)
        {
            var currentTime = Time.time;
            _hitEvent= hitEvent;
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
        }
        public void OnTriggerEnter2D(Collider2D collider)
        {
            if(_hitEvent != null)
            {
                _hitEvent.Invoke(collider);
            }
        }
    }
}
