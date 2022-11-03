using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public Transform weaponMuzzle;
    public GameObject impactVfx;

    // Todo : ���߿��� Pooling���� ������ ��.
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
        // �Ѿ��� ������ ����
        Vector3 shotDirection = GetShotDirectionWithinSpread(weaponMuzzle);
        // TODO : Projectile ������ �� (���⼭ Pooling�� �����)
        
        Projectile bullet = Instantiate(bulletPrefab, weaponMuzzle.position, Quaternion.LookRotation(shotDirection));
        bullet.Shoot(this);

        // Todo: Muzzle Flash (�̹� ����Ʈ�� ����)
        if (null == _muzzleFlash)
        {

        }
        else
        {

        }

        _lastTimeShot = Time.time;

    }

    // ź�� ������ ���� �����Ѵ�.
    public Vector3 GetShotDirectionWithinSpread(Transform shootTransform)
    {
        float spreadAngleRatio = bulletSpreadAngle / 180f;
        Vector3 spreadWorldDirection = Vector3.Slerp(shootTransform.forward, Random.insideUnitSphere, spreadAngleRatio);

        return spreadWorldDirection;
    }
}
