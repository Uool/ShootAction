using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeWeaponController : Weapon
{
    public Transform weaponMuzzle;
    public GameObject _muzzleFlash;
    public Projectile bulletPrefab;

    public float bulletSpreadAngle = 0f;

    private void Start()
    {
        //_muzzleFlash = Resources.Load<GameObject>("");
    }

    public override void HandleAttack()
    {
        // �Ѿ��� ������ ����
        Vector3 shotDirection = GetShotDirectionWithinSpread(weaponMuzzle);
        // TODO : Projectile ������ �� (���⼭ Pooling�� �����)

        //Projectile bullet = Instantiate(bulletPrefab, weaponMuzzle.position, Quaternion.LookRotation(shotDirection));
        Projectile bullet = Managers.Resource.Instantiate(bulletPrefab.gameObject, weaponMuzzle.position, Quaternion.LookRotation(shotDirection)).GetComponent<Projectile>();
        bullet?.Shoot(this);

        // Todo: Muzzle Flash (�̹� ����Ʈ�� ����)
        if (null == _muzzleFlash)
        {

        }
        else
        {

        }

        _lastTimeAttack = Time.time;
    }

    public void EndShoot()
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
