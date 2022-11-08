using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public GameObject impactVfxPrefab;
    public float delayBetweenAttack = 0.5f;

    protected AudioSource _attackSound;
    protected float _lastTimeAttack = Mathf.NegativeInfinity;

    public float Damage { get { return _damage; } }
    protected float _damage = 10f;

    public void Init(float damage, float attackDelayTime)
    {
        _damage = damage;
        delayBetweenAttack = attackDelayTime;
    }

    public bool TryAttack()
    {
        if (_lastTimeAttack + delayBetweenAttack < Time.time)
        {
            HandleAttack();
            return true;
        }
        return false;
    }

    public abstract void HandleAttack();

}
