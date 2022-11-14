using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float maxLifeTime = 3f;
    public float shootSpeed = 20f;
    public float radius = 0.001f;
    public LayerMask hitTableLayers = -1;

    // Bullet
    private Vector3 _velocity;
    private float _damage;
    private TrailRenderer _trail;
    private WaitForSeconds _delayTime;
    // Vfx
    private GameObject _impactVfxPrefab;
    private float _impactVfxLifeTime = 2.5f;

    private void OnEnable()
    {
        if (null == _delayTime)
            _delayTime = new WaitForSeconds(maxLifeTime);

        if (null == _trail)
            _trail = GetComponentInChildren<TrailRenderer>();

        
        StopCoroutine("coDelayDestroy");
        StartCoroutine(coDelayDestroy());
    }

    public void Shoot(RangeWeaponController controller)
    {
        _impactVfxPrefab = controller.impactVfxPrefab;
        _damage = controller.Damage;
        _velocity = transform.forward * shootSpeed;
    }

    private void Update()
    {
        transform.position += _velocity * Time.deltaTime;

        RaycastHit closestHit = new RaycastHit();
        closestHit.distance = Mathf.Infinity;
        bool foundHit = false;

        RaycastHit[] hits = Physics.SphereCastAll(transform.position, radius,
            transform.forward.normalized, _velocity.magnitude,
            hitTableLayers, QueryTriggerInteraction.Collide);

        foreach (var hit in hits)
        {
            if (IsHitValid(hit) && hit.distance < 0.2f && hit.distance < closestHit.distance)
            {
                foundHit = true;
                closestHit = hit;
            }
        }

        if (true == foundHit)
        {
            OnHit(closestHit.point, closestHit.normal, closestHit.collider);
        }
    }

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
            damageable.InflictDamage(_damage);
        }

        if (true == _impactVfxPrefab)
        {
            GameObject impact = Instantiate(_impactVfxPrefab, point, Quaternion.identity);
            if (_impactVfxLifeTime > 0f)
            {
                Destroy(impact.gameObject, _impactVfxLifeTime);
            }
        }
        _trail.Clear();
        Managers.Resource.Destroy(this.gameObject);
    }

    IEnumerator coDelayDestroy()
    {
        yield return _delayTime;
        _trail.Clear();
        Managers.Resource.Destroy(gameObject);
    }
}
