using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHeath : MonoBehaviour , IDamageable
{
    [Header("Health Settings")]
    [SerializeField] private int health;
    [SerializeField] private SkinnedMeshRenderer enemyRenderer;
    [SerializeField] private EnemyAnimation enemyAnimation;

    [Header("Health UI")]
    [SerializeField] private GameObject _helathCanvas;
    [SerializeField] private Image _healthFill;
    

    private Material originalMaterial;
    private Coroutine flashCoroutine;

    bool isDead = false;

    public event Action OnDeath;
    private void Start()
    {
        HideHealthCanvas();
    }

    public virtual void TakeDamage(int amount)
    {
        UnhideHealthCanvas();
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
    void HideHealthCanvas()
    {
        _helathCanvas.gameObject.SetActive(false);
    }
    void UnhideHealthCanvas()
    {
        _helathCanvas.gameObject.SetActive(true);
    }
}
