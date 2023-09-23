using Task.Interfaces;
using UnityEngine;

public class Hitbox : MonoBehaviour
{
    [SerializeField]
    private GameObject _heatObject;
    private IHeat _entity;
    private void Start()
    {
        if(!_heatObject.TryGetComponent(out _entity))
        {
            Debug.LogError("heat entity not found", this);
            throw new System.Exception("heat entity not found");
        }
    }
    public void Heat(float damage)
    {
        _entity.Heat(damage);
    }
}
