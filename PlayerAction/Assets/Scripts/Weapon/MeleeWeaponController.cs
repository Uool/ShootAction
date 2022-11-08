using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeaponController : Weapon
{
    private void Start()
    {
        
    }
    public override void HandleAttack()
    {
        
    }

    // TODO: 모든 공격에 들어가긴 하는데..
    bool IsHitValid(RaycastHit hit)
    {
        if (hit.collider.isTrigger && hit.collider.GetComponent<Damageable>())
            return false;

        return true;
    }

    void OnHit(Vector3 point, Vector3 normal, Collider collider)
    {
        if (point == Vector3.zero)
            return;

        Damageable damageable = collider.GetComponent<Damageable>();
        if (true == damageable)
        {
            //TODO: 체력이 감소되어야 한다.
            damageable.InflictDamage(Damage);
        }

        if (true == impactVfxPrefab)
        {
            GameObject impact = Instantiate(impactVfxPrefab, point, Quaternion.identity);

            //if (_impactVfxLifeTime > 0f)
            //{
            //    Destroy(impact.gameObject, _impactVfxLifeTime);
            //}
        }
        Destroy(this.gameObject);
    }
}
