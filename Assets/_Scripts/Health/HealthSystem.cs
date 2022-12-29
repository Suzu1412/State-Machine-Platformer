using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HealthSystem : MonoBehaviour, IHittable, IHealable
{
    [SerializeField] private int maxHealth;
    [SerializeField] private int currentHealth;
    private float invulnerabilityDuration;

    public UnityEvent OnAddHealth;

    public UnityEvent<int> OnHealthValueChanged;
    public UnityEvent<int> OnInitializeMaxHealth;

    public event Action OnHit;
    public event Action OnDeath;

    private Coroutine InvulnerabilityCoroutine;

    public int CurrentHealth
    {
        get => currentHealth;
        set
        {
            currentHealth = value;
            OnHealthValueChanged?.Invoke(currentHealth);
        }
    }

    public bool isInvulnerable;

    public void GetHit(GameObject gameObject, int weaponDamage)
    {
        GetHit(weaponDamage);
    }

    public void GetHit(int amount)
    {
        if (isInvulnerable) return;

        CurrentHealth = Mathf.Clamp(currentHealth - amount, 0, maxHealth);

        if (currentHealth <= 0)
        {
            OnDeath?.Invoke();
        }
        else
        {
            OnHit?.Invoke();
            InvulnerabilityCoroutine = StartCoroutine(InvulnerabilityPeriod());
        }

    }

    public void Heal(int amount)
    {
        CurrentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        OnAddHealth?.Invoke();
    }

    public void Initialize(int health, float invulnerabilityDuration)
    {
        maxHealth = health;
        this.invulnerabilityDuration = invulnerabilityDuration;
        OnInitializeMaxHealth?.Invoke(maxHealth);
        CurrentHealth = maxHealth;
    }

    private IEnumerator InvulnerabilityPeriod()
    {
        isInvulnerable = true;
        yield return new WaitForSeconds(invulnerabilityDuration);
        isInvulnerable = false;
    }
}
