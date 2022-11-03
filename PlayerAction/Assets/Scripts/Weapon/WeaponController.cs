using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public Transform weaponMuzzle;
    public GameObject impactVfx;

    // Todo : 나중에는 Pooling으로 만들어야 함.
    public Projectile bulletPrefab;

    public float Damage { get { return _damage; } }
    private float _damage;

    public float delayBetweenShots = 0.5f;
    public float bulletSpreadAngle = 0f;

    private GameObject _muzzleFlash;
    private AudioSource _shootAudioSource;
    private float _lastTimeShot = Mathf.NegativeInfinity;

    private void Awake()
    {
        _shootAudioSource = GetComponent<AudioSource>();
        //_muzzleFlash = Resources.Load<GameObject>("");
    }


    public bool TryShoot()
    {
        if (_lastTimeShot + delayBetweenShots < Time.time)
        {
            HandleShoot();
            return true;
        }
        return false;
    }

    void HandleShoot()
    {
        // 총알이 나가는 방향
        Vector3 shotDirection = GetShotDirectionWithinSpread(weaponMuzzle);
        // TODO : Projectile 만들어야 함 (여기서 Pooling을 써야함)
        
        Projectile bullet = Instantiate(bulletPrefab, weaponMuzzle.position, Quaternion.LookRotation(shotDirection));
        bullet.Shoot(this);

        // Todo: Muzzle Flash (이미 이펙트로 있음)
        if (null == _muzzleFlash)
        {

        }
        else
        {

        }

        _lastTimeShot = Time.time;

    }

    // 탄이 퍼지는 것을 관리한다.
    public Vector3 GetShotDirectionWithinSpread(Transform shootTransform)
    {
        float spreadAngleRatio = bulletSpreadAngle / 180f;
        Vector3 spreadWorldDirection = Vector3.Slerp(shootTransform.forward, Random.insideUnitSphere, spreadAngleRatio);

        return spreadWorldDirection;
    }
}
