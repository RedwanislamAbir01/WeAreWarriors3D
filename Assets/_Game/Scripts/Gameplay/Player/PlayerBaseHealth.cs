using _Game.Managers;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBaseHealth : MonoBehaviour, IDamageable
{
    [SerializeField] private int maxHealth = 500; // Maximum health value
    private int currentHealth;
    [SerializeField] private MeshRenderer baseRenderer;

    [Header("Health UI")]
    [SerializeField] private Image _healthFill;
    [SerializeField]
    private TextMeshProUGUI _healthText;

    private Canvas _healthCanvas;
    private Material originalMaterial;
    private Coroutine flashCoroutine;


    bool isDead = false;

    public event Action OnDeath;
    private void Start()
    {
        currentHealth = maxHealth;
        _healthText.text = currentHealth.ToString();
        _healthCanvas = GetComponentInChildren<Canvas>();   
    }
    public virtual void TakeDamage(int amount)
    {
        currentHealth -= amount;
        _healthText.text = currentHealth.ToString();
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
    protected virtual void Die()
    {
        OnDeath?.Invoke();
        isDead = true;
        IsDead();
        _healthCanvas.gameObject.SetActive(false);
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
