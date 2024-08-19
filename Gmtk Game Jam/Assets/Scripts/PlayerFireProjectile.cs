using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFireProjectile : MonoBehaviour
{
    [SerializeField]
    private GameObject _projectile;
    public void SpawnProjectile()
    {
        Debug.Log("spawned");
        Instantiate( _projectile, transform.position, Quaternion.identity);
    }
}
