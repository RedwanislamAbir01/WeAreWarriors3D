using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    [SerializeField] private TextMeshProUGUI _healthReduceFeedBackPrefab;
    [SerializeField] private Transform _healthReduceFeedBackTr;

    [Header("Health Settings")]
    [SerializeField] private int maxHealth = 100; // Maximum health value
    private int currentHealth;
    [SerializeField] private SkinnedMeshRenderer playerRenderer;
    [SerializeField] private PlayerAnimation playerAnimation;
    [SerializeField] private Transform renderTransform; // New field for render transform

    [Header("Health UI")]
    [SerializeField] private GameObject _helathCanvas;
    [SerializeField] private Image _healthFill;

    private Material originalMaterial;
    private Coroutine flashCoroutine;
    private bool isDead = false;

    private Vector3 originalScale;

    public event Action OnDeath;

    private void Start()
    {
        currentHealth = maxHealth;
        HideHealthCanvas();
        originalScale = renderTransform.localScale;
    }

    public virtual void TakeDamage(int amount)
    {
        UnhideHealthCanvas();
        currentHealth -= amount;
        TextPop();
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

            // Apply squash and stretch effect
            StartCoroutine(SquashAndStretch());
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
        yield return new WaitForSeconds(0.1f);

        // Reset back to original color
        playerRenderer.material.SetColor("_EmissionColor", originalEmissionColor);
        flashCoroutine = null;
    }

    private IEnumerator SquashAndStretch()
    {
        float duration = 0.1f;
        float squashFactor = 0.5f; // Adjust squash factor as needed

        Vector3 targetScale = originalScale;
        targetScale.y *= squashFactor;

        // Squash
        float elapsedTime = 0f;
        while (elapsedTime < duration / 2)
        {
            renderTransform.localScale = Vector3.Lerp(originalScale, targetScale, elapsedTime / (duration / 2));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Stretch back
        elapsedTime = 0f;
        while (elapsedTime < duration / 2)
        {
            renderTransform.localScale = Vector3.Lerp(targetScale, originalScale, elapsedTime / (duration / 2));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        renderTransform.localScale = originalScale; // Ensure original scale is set
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
    void TextPop()
    {
        TextMeshProUGUI healthReduceFeedBack = Instantiate(_healthReduceFeedBackPrefab, _healthReduceFeedBackTr);
        healthReduceFeedBack.text = "-10";
        healthReduceFeedBack.rectTransform
            .DOAnchorPos(new Vector3(UnityEngine.Random.Range(-30, 30f), healthReduceFeedBack.rectTransform.localPosition.y + 40, 0), .5f)
            .OnComplete(() => { Destroy(healthReduceFeedBack.gameObject); });
    }
}
