using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterEffects : MonoBehaviour
{
    [SerializeField] private GameObject _deathEffect;
    [SerializeField] private GameObject _bloodEffect;
    IDamageable damagableCharacter;
    void Start()
    {
     damagableCharacter = GetComponent<IDamageable>();

     damagableCharacter.OnDeath += DamagableCharacter_OnDeath;
    }
    private void OnDestroy()
    {
        damagableCharacter.OnDeath -= DamagableCharacter_OnDeath;
    }


    void DamagableCharacter_OnDeath()
    {
        Instantiate(_deathEffect, new Vector3(transform.position.x , 1 , transform.position.z), Quaternion.identity);
        Instantiate(_bloodEffect, new Vector3(transform.position.x, 1, transform.position.z), Quaternion.identity);
    }
}
