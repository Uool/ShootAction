using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public float bulletSpreadAngle = 0f;

    private void Awake()
    {
        
    }


    bool TryShoot()
    {
        return false;
    }

    void HandleShoot()
    {

    }

    void UpdateShootSound()
    {

    }

    // ź�� ������ ���� �����Ѵ�.
    public Vector3 GetShotDirectionWithinSpread(Transform shootTransform)
    {
        float spreadAngleRatio = bulletSpreadAngle / 180f;
        Vector3 spreadWorldDirection = Vector3.Slerp(shootTransform.forward, Random.insideUnitSphere, spreadAngleRatio);

        return spreadWorldDirection;
    }
}
