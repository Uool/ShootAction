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
        // TODO: ���� ����

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
                // ���� ĳ��Ʈ�� ���� 0 ���ʹ� �浹�ڰ� �ݰ濡 ���� ������ "��"�� ��ġ���� ����� �������� �ʽ��ϴ�.
                // �׷��� �츮�� "������" �� ĳ��Ʈ�� ��ġ�� ��� ���� ���� �� �ֵ��� ���� ���� �̽��� ���� ĳ��Ʈ�� �����մϴ�.
                // TODO : ����������?
                if (attackVector.magnitude < 0.001f)
                {
                    attackVector = Vector3.forward * 0.0001f;
                }

                Ray ray = new Ray(worldPos, attackVector.normalized);

                // Physics.SphereCastNonAlloc -> ����� �Ÿ��� �˷��ָ�, ���� �ش� ����� �Ÿ��� �̵��� ������ ��ġ�� Collider�� �˻��Ѵ�.
                int contacts = Physics.SphereCastNonAlloc(ray, pts.radius, _raycastHitCache, attackVector.magnitude,
                    ~0, QueryTriggerInteraction.Ignore);

                for (int j = 0; j < contacts; j++)
                {
                    Collider collider = _raycastHitCache[j].collider;
                    if (null != collider)
                        isDamaged = CheckDamage(collider, pts);
                }
                // ���⸦ �ֵθ��鼭 ����� ��ġ�� �������ش�.
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

        // TODO: Hit Sound ������ �־ �ǰ�~

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
