using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationEvent : MonoBehaviour
{
    [SerializeField] private Weapon weapon;
    public void EnableWeaponCollider()
    {
        weapon.GetComponent<Collider>().enabled = true;
    }
    public void DisableWeaponCollider()
    {
        weapon.GetComponent<Collider>().enabled = false;
    }

}