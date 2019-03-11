using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyableBloc : HittableEntity
{
    protected override void Die()
    {
        isAlive = false;
        Destroy(gameObject);
    }

    protected override void Hit()
    {
        Debug.Log("Hit");
    }

    protected override void Hit(RaycastHit hit)
    {
        
    }
}
