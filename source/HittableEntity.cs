using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//mother class of all entity having to deal with life, death and hit

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

    //when entity takes damage, this method is used for FX handling (blood spray, etc)
    protected abstract void Hit();

    //same as above but this one is called when the damage comes from a rayCast wich means a bullet
    //used to distinguish different types of dmg
    protected abstract void Hit(RaycastHit hit);

    protected abstract void Die();

    public float getCurrentHp()
    {
        return currentHp;
    }
}
