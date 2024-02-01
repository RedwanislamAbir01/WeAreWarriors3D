using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
  
    // Damage amount of the weapon
    public int damageAmount;

    IDamageable damageable;
    // Start is called before the first frame update
    void Start()
    {
        damageable = GetComponentInParent<IDamageable>();

        damageable.OnDeath += HideObj;
    }

   
     void HideObj() => gameObject.SetActive(false);
}