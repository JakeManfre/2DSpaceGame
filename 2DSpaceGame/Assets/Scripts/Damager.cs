using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damager : MonoBehaviour
{
    [Header("Properties")]
    [SerializeField]
    float damageDone;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Damageable damageable = collision.GetComponent<Damageable>();
        if (!damageable) { return; }

        damageable.takeDamage(damageDone);
        Destroy(gameObject);
    }
}
