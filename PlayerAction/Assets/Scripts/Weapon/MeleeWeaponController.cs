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

    // TODO: ��� ���ݿ� ���� �ϴµ�..
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
            //TODO: ü���� ���ҵǾ�� �Ѵ�.
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
