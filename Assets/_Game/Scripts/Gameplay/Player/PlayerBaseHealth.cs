using _Game.Managers;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBaseHealth : MonoBehaviour, IDamageable
{
    [SerializeField] private int health;
    [SerializeField] private MeshRenderer baseRenderer;

    [Header("Health UI")]
    [SerializeField] private Image _healthFill;


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
        StartCoroutine(UpdateHealthFill());
    }
    private IEnumerator UpdateHealthFill()
    {
        // Change fill color to white while reducing
        _healthFill.color = Color.white;

        float originalFillAmount = _healthFill.fillAmount;
        float targetFillAmount = (float)health / 100f; // Assuming health ranges from 0 to 100
        float fillSpeed = 0.5f; // Adjust speed as needed

        while (_healthFill.fillAmount > targetFillAmount)
        {
            _healthFill.fillAmount -= fillSpeed * Time.deltaTime;
            yield return null;
        }

        // Reset fill color to default
        _healthFill.color = Color.red; // Adjust to your default color
    }
    protected virtual void Die()
    {
        OnDeath?.Invoke();
        isDead = true;
        IsDead();
        GameManager.Instance.LevelFail();

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
