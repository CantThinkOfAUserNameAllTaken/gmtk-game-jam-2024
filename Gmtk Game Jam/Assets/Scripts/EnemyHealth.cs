using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : BaseHealth
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    protected override void Death()
    {
        base.Death();
        Destroy(gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
