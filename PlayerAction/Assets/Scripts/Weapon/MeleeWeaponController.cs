using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeaponController : Weapon
{
    [System.Serializable]
    public class AttackPoint
    {
        public float radius;
        public Vector3 offset;
        public Transform attackRoot;
    }

    public AttackPoint[] attackPoints = new AttackPoint[0];
    public LayerMask targetLayerMask;

    private Vector3[] _previousPos = null;
    private Vector3 _direction;

    private bool _isAttacking;

    private RaycastHit[] _raycastHitCache = new RaycastHit[32];
    private Collider[] _colliderCache = new Collider[32];

    public override void HandleAttack()
    {
        // TODO: 공격 사운드

        _isAttacking = true;
        _previousPos = new Vector3[attackPoints.Length];

        for (int i = 0; i < attackPoints.Length; i++)
        {
            Vector3 worldPos = attackPoints[i].attackRoot.position + attackPoints[i].attackRoot.TransformVector(attackPoints[i].offset);
            _previousPos[i] = worldPos;
        }

    }

    public void EndAttack()
    {
        _isAttacking = false;
    }

    private void FixedUpdate()
    {
        if (true == _isAttacking)
        {
            for (int i = 0; i < attackPoints.Length; i++)
            {
                AttackPoint pts = attackPoints[i];
                Vector3 worldPos = pts.attackRoot.position + pts.attackRoot.TransformVector(pts.offset);
                Vector3 attackVector = worldPos - _previousPos[i];
                bool isDamaged = false;
                // 구형 캐스트에 대한 0 벡터는 충돌자가 반경에 의해 생성된 "구"와 겹치더라도 결과를 산출하지 않습니다.
                // 그래서 우리는 "고정된" 구 캐스트와 겹치는 모든 것을 잡을 수 있도록 아주 작은 미시적 전방 캐스트를 설정합니다.
                // TODO : 무슨뜻이지?
                if (attackVector.magnitude < 0.001f)
                {
                    attackVector = Vector3.forward * 0.0001f;
                }

                Ray ray = new Ray(worldPos, attackVector.normalized);

                // Physics.SphereCastNonAlloc -> 방향과 거리를 알려주면, 구가 해당 방향과 거리로 이동한 궤적에 겹치는 Collider를 검사한다.
                int contacts = Physics.SphereCastNonAlloc(ray, pts.radius, _raycastHitCache, attackVector.magnitude,
                    ~0, QueryTriggerInteraction.Ignore);

                for (int j = 0; j < contacts; j++)
                {
                    Collider collider = _raycastHitCache[j].collider;
                    if (null != collider)
                        isDamaged = CheckDamage(collider, pts);
                }
                // 무기를 휘두르면서 변경된 위치를 저장해준다.
                _previousPos[i] = worldPos;
                if (isDamaged)
                {
                    _isAttacking = false;
                    break;
                }
            }
        }
    }

    private bool CheckDamage(Collider other, AttackPoint pts)
    {
        Damageable damageable = other.GetComponent<Damageable>();
        if (null == damageable)
            return false;

        if (damageable.gameObject == _Owner)
            return false;

        // TODO: Hit Sound 있으면 넣어도 되고~

        damageable.InflictDamage(Damage);
        return true;
    }

    private void OnDrawGizmosSelected()
    {
        for (int i = 0; i < attackPoints.Length; i++)
        {
            AttackPoint pts = attackPoints[i];

            if (null != pts.attackRoot)
            {
                Vector3 worldPos = pts.attackRoot.TransformVector(pts.offset);
                Gizmos.color = new Color(1f, 1f, 1f, 0.4f);
                Gizmos.DrawSphere(pts.attackRoot.position + worldPos, pts.radius);
            }
        }
    }

}
