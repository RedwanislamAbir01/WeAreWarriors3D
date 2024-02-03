using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEffects : MonoBehaviour
{
    [SerializeField] private GameObject _deathEffect;
    private int ySinkAmmount = -5;
    IDamageable damagableBase;
    void Start()
    {
        damagableBase = GetComponent<IDamageable>();

        damagableBase.OnDeath += DamagableCharacter_OnDeath;
    }
    private void OnDestroy()
    {
        damagableBase.OnDeath -= DamagableCharacter_OnDeath;
    }


    void DamagableCharacter_OnDeath()
    {
        Instantiate(_deathEffect, new Vector3(transform.position.x, 1, transform.position.z), Quaternion.identity);
        SinkInGround();
    }

    void SinkInGround()
    {
        transform.DOMoveY(ySinkAmmount, .5f);
    }
}
