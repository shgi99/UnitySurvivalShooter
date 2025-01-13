using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivingEntity : MonoBehaviour, IDamageable
{
    public float maxHp = 100f;
    public float Hp { get; protected set; }
    public bool IsDead { get; protected set; }
    public event Action onDeath;
    protected virtual void OnEnable()
    {
        IsDead = false;
        Hp = maxHp;
    }
    public virtual void OnDamage(float damage, Vector3 hitPoint, Vector3 hitNormal)
    {
        Hp -= damage;
        if(Hp <= 0 && !IsDead)
        {
            Die();
        }
    }
    public virtual void Die()
    {
        onDeath?.Invoke();
        IsDead = true;
        Hp = 0;
    }
}
