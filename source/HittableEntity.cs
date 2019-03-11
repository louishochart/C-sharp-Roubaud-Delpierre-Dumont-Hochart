using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class HittableEntity : MonoBehaviour
{
    //life handler
    public float maxHp;
    protected float currentHp;
    protected bool isAlive = true;
    protected bool isInvincible = false;

    protected void Start()
    {
        
        currentHp = maxHp;
    }

    public void TakeDamage(float dmg)
    {
        if (isInvincible) return;
        Hit();
        currentHp -= (int)dmg;
        if (currentHp <= 0)
        {
            currentHp = 0;
            Die();
        }
    }

    public void setMaxHP(float hp)
    {
        if (hp < 1) hp = 1;

        maxHp = hp;
        currentHp = hp;
    }

    public void TakeDamage(float dmg, RaycastHit hit)
    {
        if (isInvincible) return;
        Hit(hit);
        currentHp -= (int)dmg;
        if (currentHp <= 0)
        {
            currentHp = 0;
            Die();
        }
    }

    protected abstract void Hit();

    protected abstract void Hit(RaycastHit hit);

    protected abstract void Die();

    public float getCurrentHp()
    {
        return currentHp;
    }
}
