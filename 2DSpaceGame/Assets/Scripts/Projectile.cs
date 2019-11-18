using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Header("Properties")]
    [SerializeField]
    int maxLifeTime;

    // Events
    public delegate void callbackOnTriggerEnter2D(Projectile projectile, Collider2D collider);
    public event callbackOnTriggerEnter2D OnTriggerEnter2D_Event;

    // Start is called before the first frame update
    void Start()
    {
        if (maxLifeTime != -1)
        {
            Destroy(gameObject, maxLifeTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (OnTriggerEnter2D_Event == null) 
        {
            Destroy(gameObject);
        }
        else
        {
            OnTriggerEnter2D_Event(this, collision);
        }
    }
}
