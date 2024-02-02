using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHeath : MonoBehaviour , IDamageable
{   

    [SerializeField] private int health;
    [SerializeField] private SkinnedMeshRenderer enemyRenderer;
    [SerializeField] private EnemyAnimation enemyAnimation;

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
        enemyAnimation.PlayDeathAnim();
        Destroy(gameObject);

    }
   

    public Transform GetTransform()
    {
        return transform;
    }
    private IEnumerator FlashMaterial()
    {
        if (enemyRenderer == null)
        {
            yield break;
        }
        Color originalEmissionColor = enemyRenderer.material.GetColor("_EmissionColor");
        // Flash white
        enemyRenderer.material.SetColor("_EmissionColor", Color.white);
        yield return new WaitForSeconds(0.05f); 

        // Reset back to original color
        enemyRenderer.material.SetColor("_EmissionColor", originalEmissionColor);
        flashCoroutine = null;
    }


    public bool IsDead()
    {
        return isDead;
    }
}
