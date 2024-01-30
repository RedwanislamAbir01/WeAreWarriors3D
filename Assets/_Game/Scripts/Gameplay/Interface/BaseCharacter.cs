using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseCharacter : MonoBehaviour
{



    public abstract void MoveTowardsTarget(Transform target);

    
    public abstract void Attack(Transform target);


}