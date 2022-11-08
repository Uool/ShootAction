using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileEffect : MonoBehaviour
{
    private ParticleSystem _mainParticle;
    private ParticleSystem[] _childrenParticles;
    private ProjectileEffectOnHit _onHit;
    // Start is called before the first frame update
    void Start()
    {
        _mainParticle = GetComponent<ParticleSystem>();
        _childrenParticles = GetComponentsInChildren<ParticleSystem>();
        _onHit = GetComponentInChildren<ProjectileEffectOnHit>();
    }

    public void BulletInit(float damage, float duration)
    {
        for (int i = 1; i < _childrenParticles.Length; i++)
        {
            var main = _childrenParticles[i].main;
            main.duration = duration;
        }

        if (null == _onHit)
        {
            Debug.LogError("ProjectileEffectOnHit is null");
            return;
        }
        _onHit.SetDamage(damage);
        //this.gameObject.SetActive(false);
    }

    public void StartParticle() 
    {
        if (_mainParticle.isPlaying)
            return;
        _mainParticle.Play();
    }
    public void StopParticle()
    {
        if (!_mainParticle.isPlaying)
            return;
        _mainParticle.Stop();
    }
}
