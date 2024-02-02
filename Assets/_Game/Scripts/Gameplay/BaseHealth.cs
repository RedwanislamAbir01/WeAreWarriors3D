using System;
using UnityEngine;

public abstract class BaseHealth : MonoBehaviour, IDamageable
{
    [SerializeField] protected int maxHealth = 100;
    protected int currentHealth;
    protected bool isDead = false;

    public event Action OnDeath;

    protected virtual void Start()
    {
        currentHealth = maxHealth;
        HideHealthCanvas();
    }

    public virtual void TakeDamage(int amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0 && !isDead)
        {
            Die();
        }
        else
        {
            FlashHealthUI();
        }

        UpdateHealthFill();
    }

    protected abstract void Die();

    protected virtual void UpdateHealthFill()
    {
        // Implement health fill update logic here
    }

    protected virtual void FlashHealthUI()
    {
        // Implement health UI flash logic here
    }

    protected virtual void HideHealthCanvas()
    {
        // Implement hiding health canvas logic here
    }

    protected virtual void UnhideHealthCanvas()
    {
        // Implement unhiding health canvas logic here
    }

    public bool IsDead()
    {
        return isDead;
    }

    public Transform GetTransform()
    {
        return transform;
    }
}
