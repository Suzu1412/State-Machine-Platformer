using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HealthSystem : MonoBehaviour, IHittable, IHealable
{
    [SerializeField] private int maxHealth;
    [SerializeField] private int currentHealth;

    public UnityEvent OnAddHealth;

    public UnityEvent<int> OnHealthValueChanged;
    public UnityEvent<int> OnInitializeMaxHealth;

    public UnityEvent OnInvulnerabilityStarted;
    public UnityEvent OnInvulnerabilityAboutToFinish;
    public UnityEvent OnInvulnerabilityFinished;

    public event Action OnHit;
    public event Action OnDeath;

    public bool IsDeath { get; private set; }
    public bool IsHit { get; private set; }

    private Coroutine invulnerabilityCoroutine;
    private Coroutine hitResetCoroutine;
    private Coroutine deathResetCoroutine;

    private WaitForSeconds waitForSeconds = new(0.15f);
    private WaitForSeconds HitStunDuration = new(0.15f);
    private WaitForSeconds invulnerabilityPeriod = new(1.5f);
    private WaitForSeconds invulnerabilityPeriodEnding = new(0.5f);


    public int CurrentHealth
    {
        get => currentHealth;
        set
        {
            currentHealth = value;
            OnHealthValueChanged?.Invoke(currentHealth);
        }
    }

    private bool isInvulnerable;
    private bool isHitStunned;
    public bool IsInvulnerable => isInvulnerable;
    public bool IsHitStunned => isHitStunned;

    public void GetHit(GameObject gameObject, int weaponDamage)
    {
        GetHit(weaponDamage);
    }

    public void GetHit(int amount)
    {
        if (isInvulnerable || IsHitStunned) return;

        CurrentHealth = Mathf.Clamp(currentHealth - amount, 0, maxHealth);

        if (currentHealth <= 0)
        {
            deathResetCoroutine = StartCoroutine(DeathResetCoroutine());
            OnDeath?.Invoke();
        }
        else
        {
            invulnerabilityCoroutine = StartCoroutine(InvulnerabilityCoroutine());
            hitResetCoroutine = StartCoroutine(HitResetCoroutine());
            OnHit?.Invoke();
        }
    }

    public void Heal(int amount)
    {
        CurrentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        OnAddHealth?.Invoke();
    }

    public float GetHealthPercent()
    {
        return maxHealth * currentHealth / 100;
    }

    public void Initialize(int health, float hitStunDuration)
    {
        maxHealth = health;
        invulnerabilityPeriod = new(1.2f);
        invulnerabilityPeriodEnding = new(0.3f);
        HitStunDuration = new(hitStunDuration);
        OnInitializeMaxHealth?.Invoke(maxHealth);
        CurrentHealth = maxHealth;
    }

    private IEnumerator InvulnerabilityCoroutine()
    {
        isInvulnerable = true;
        OnInvulnerabilityStarted?.Invoke();
        yield return invulnerabilityPeriod;
        OnInvulnerabilityAboutToFinish?.Invoke();
        yield return invulnerabilityPeriodEnding;
        isInvulnerable = false;
        OnInvulnerabilityFinished?.Invoke();
    }

    private IEnumerator HitResetCoroutine()
    {
        IsHit = true;
        isHitStunned = true;
        yield return HitStunDuration;
        IsHit = false;
        isHitStunned = false;
    }

    private IEnumerator DeathResetCoroutine()
    {
        IsDeath = true;
        yield return waitForSeconds;
        IsDeath = false;
    }
}
