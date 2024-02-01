using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBaseHealth : MonoBehaviour, IDamageable
{
    [SerializeField] private int health;
    [SerializeField] private MeshRenderer baseRenderer;

    private Material originalMaterial;
    private Coroutine flashCoroutine;

    bool isDead = false;

    public event Action OnDeath;

    public virtual void TakeDamage(int amount)
    {
        health -= amount;
        if (health <= 0)
        {
            Die();
        }
        else
        {
            if (flashCoroutine != null)
            {
                StopCoroutine(flashCoroutine);
            }
            flashCoroutine = StartCoroutine(FlashMaterial());
        }
    }

    protected virtual void Die()
    {
        OnDeath?.Invoke();
        isDead = true;
        IsDead();


    }


    public Transform GetTransform()
    {
        return transform;
    }
    private IEnumerator FlashMaterial()
    {
        if (baseRenderer == null)
        {
            yield break;
        }
        Color originalEmissionColor = baseRenderer.material.GetColor("_EmissionColor");
        // Flash white
        baseRenderer.material.SetColor("_EmissionColor", Color.white);
        yield return new WaitForSeconds(0.1f);

        // Reset back to original color
        baseRenderer.material.SetColor("_EmissionColor", originalEmissionColor);
        flashCoroutine = null;
    }

    public bool IsDead()
    {
        return isDead;
    }
}
