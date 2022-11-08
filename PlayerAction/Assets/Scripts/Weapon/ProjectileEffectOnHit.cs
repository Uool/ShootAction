using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileEffectOnHit : MonoBehaviour
{
    private float _damage;
    public float Damage { get { return _damage; } }

    public void SetDamage(float damage) { _damage = damage; }
    private void OnParticleCollision(GameObject other)
    {
        Debug.Log($"맞음! 맞은 오브젝트 : {other.name}");

        Damageable damageable = other.GetComponent<Damageable>();
        if (damageable)
        {
            // Todo: 데미지 전달

        }
    }
}
