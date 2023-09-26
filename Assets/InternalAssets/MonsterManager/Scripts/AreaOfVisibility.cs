using System.Collections;
using System.Collections.Generic;
using Task.Interfaces;
using UnityEngine;

public class AreaOfVisibility : MonoBehaviour
{
    [SerializeField]
    private GameObject _monsterObject;
    private IMonster _monster;
    private void Awake()
    {
        if(!(_monsterObject.TryGetComponent(out _monster)))
        {
            Debug.LogError("Monster not found",this);
            throw new System.Exception("Monster not found");
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            _monster.Chase(collision.transform);
        }
    }
}
