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
        // 총알이 나가는 방향
        Vector3 shotDirection = GetShotDirectionWithinSpread(weaponMuzzle);
        // TODO : Projectile 만들어야 함 (여기서 Pooling을 써야함)

        //Projectile bullet = Instantiate(bulletPrefab, weaponMuzzle.position, Quaternion.LookRotation(shotDirection));
        Projectile bullet = Managers.Resource.Instantiate(bulletPrefab.gameObject, weaponMuzzle.position, Quaternion.LookRotation(shotDirection)).GetComponent<Projectile>();
        bullet?.Shoot(this);

        // Todo: Muzzle Flash (이미 이펙트로 있음)
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

    // 탄이 퍼지는 것을 관리한다.
    public Vector3 GetShotDirectionWithinSpread(Transform shootTransform)
    {
        float spreadAngleRatio = bulletSpreadAngle / 180f;
        Vector3 spreadWorldDirection = Vector3.Slerp(shootTransform.forward, Random.insideUnitSphere, spreadAngleRatio);

        return spreadWorldDirection;
    }
}
