using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Damageable))]
public class Health : MonoBehaviour
{
    public float maxHealth = 100f;

    public UnityAction<float> onDamaged;
    public UnityAction<float> onHealed;
    public UnityAction onDie;

    public float CurrentHealth { get; set; }
    public bool Invincible { get; set; }


    public bool CanPickup() => CurrentHealth < maxHealth;
    public float GetRatio() => CurrentHealth / maxHealth;

    bool _isDead;

    private void Start()
    {
        CurrentHealth = maxHealth;
    }

    public void Heal(float healAmount)
    {
        float healthBefore = CurrentHealth;
        CurrentHealth += healAmount;
        CurrentHealth = Mathf.Clamp(CurrentHealth, 0f, maxHealth);

        float trueHealAmount = CurrentHealth - healthBefore;
        if (trueHealAmount > 0f)
            onHealed?.Invoke(trueHealAmount);
    }

    public void TakeDamage(float damage)
    {
        if (Invincible)
            return;

        float healthBefore = CurrentHealth;
        CurrentHealth -= damage;
        CurrentHealth = Mathf.Clamp(CurrentHealth, 0f, maxHealth);

        float trueHealAmount = healthBefore - CurrentHealth;
        if (trueHealAmount > 0f)
            onDamaged?.Invoke(damage);

        Debug.Log($"Object Name : {gameObject.name}, Health : {CurrentHealth}");

        HandleDeath();
    }

    void HandleDeath()
    {
        if (_isDead)
            return;

        if (CurrentHealth <= 0f)
        {
            _isDead = true;
            onDie?.Invoke();
        }
    }
}
