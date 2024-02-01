using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamageable
{

    [SerializeField] private int health;
    [SerializeField] private SkinnedMeshRenderer playerRenderer;
    [SerializeField] private PlayerAnimation playerAnimation;

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
        playerAnimation.PlayDeathAnim();
        Debug.Log(gameObject.name + " has died!");

    }


    public Transform GetTransform()
    {
        return transform;
    }
    private IEnumerator FlashMaterial()
    {
        if (playerRenderer == null)
        {
            yield break;
        }
        Color originalEmissionColor = playerRenderer.material.GetColor("_EmissionColor");
        // Flash white
        playerRenderer.material.SetColor("_EmissionColor", Color.white);
        yield return new WaitForSeconds(0.1f);

        // Reset back to original color
        playerRenderer.material.SetColor("_EmissionColor", originalEmissionColor);
        flashCoroutine = null;
    }

    public bool IsDead()
    {
        return isDead;
    }
}