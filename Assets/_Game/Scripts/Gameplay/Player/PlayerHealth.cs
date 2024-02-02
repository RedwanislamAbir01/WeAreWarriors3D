using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    [Header("Health Settings")]
    [SerializeField] private int maxHealth = 100; // Maximum health value
    private int currentHealth;
    [SerializeField] private SkinnedMeshRenderer playerRenderer;
    [SerializeField] private PlayerAnimation playerAnimation;

    [Header("Health UI")]
    [SerializeField] private GameObject _helathCanvas;
    [SerializeField] private Image _healthFill;

    private Material originalMaterial;
    private Coroutine flashCoroutine;

    bool isDead = false;

    public event Action OnDeath;
    private void Start()
    {
        currentHealth = maxHealth;
        HideHealthCanvas();
    }
    public virtual void TakeDamage(int amount)
    {
        UnhideHealthCanvas();
        currentHealth -= amount;
        if (currentHealth <= 0)
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

    protected virtual void Die()
    {
        OnDeath?.Invoke();  
        isDead = true;
        IsDead();
        playerAnimation.PlayDeathAnim();
        Destroy(gameObject);

    }


    public Transform GetTransform()
    {
        return transform;
    }
    private IEnumerator UpdateHealthFill()
    {
        // Change fill color to white while reducing
        _healthFill.color = Color.white;

        float originalFillAmount = _healthFill.fillAmount;
        float targetFillAmount = (float)currentHealth / maxHealth;
        float fillSpeed = 0.5f; // Adjust speed as needed

        while (_healthFill.fillAmount > targetFillAmount)
        {
            _healthFill.fillAmount -= fillSpeed * Time.deltaTime;
            yield return null;
        }

        // Reset fill color to default
        _healthFill.color = Color.red; // Adjust to your default color
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
        yield return new WaitForSeconds(0.05f);

        // Reset back to original color
        playerRenderer.material.SetColor("_EmissionColor", originalEmissionColor);
        flashCoroutine = null;
    }

    public bool IsDead()
    {
        return isDead;
    }

    void HideHealthCanvas()
    {
        _helathCanvas.gameObject.SetActive(false);  
    }
    void UnhideHealthCanvas()
    {
        _helathCanvas.gameObject.SetActive(true);
    }
}
