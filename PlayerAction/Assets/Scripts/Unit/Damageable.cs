using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damageable : MonoBehaviour
{
    public float damageMultiplier = 1f;

    public Health Health { get; private set; }

    // Start is called before the first frame update
    void Awake()
    {
        Health = GetComponent<Health>();
        if (null == Health)
            Debug.LogError($"{gameObject.name} / Health is Null");
    }

    // 일단 폭팔 데미지는 없다.
    public void InflictDamage(float damage, bool isExplosionDamage = false)
    {
        if (null != Health)
        {
            var totalDamage = damage;

            totalDamage *= damage;
            //if (!isExplosionDamage)
            //    totalDamage *= damageMultiplier;

            Health.TakeDamage(damage);            
        }
    }
}
