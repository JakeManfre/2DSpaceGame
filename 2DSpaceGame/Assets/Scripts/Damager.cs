using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damager : MonoBehaviour
{
    [Header("Properties")]
    [SerializeField]
    float damageDone;

    public float getDamageDone()
    {
        return damageDone;
    }
}
