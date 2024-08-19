using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerFireProjectile : MonoBehaviour
{
    [SerializeField]
    private GameObject _projectile;

    [SerializeField]
    private float _shootingDelay;

    private float _timeLastShot = -888;

    private enum SizeState 
    {
        Grown,
        Shrunk
    }

    private SizeState _state = SizeState.Grown;

    public void SpawnProjectile()
    {
        if (_state == SizeState.Grown && Time.time - _shootingDelay > _timeLastShot)
        {
            _timeLastShot = Time.time;
            Instantiate(_projectile, transform.position, Quaternion.identity);
        }

    }

    public void SetShrunk()
    {
        _state = SizeState.Shrunk;
    }

    public void SetGrown()
    {
        _state = SizeState.Grown;
    }





}
