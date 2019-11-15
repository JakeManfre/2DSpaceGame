using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    public delegate void _onTriggerEnter2D(Projectile me, Collider2D collision);

    private _onTriggerEnter2D onTriggerCallback;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 10);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setOnCollision2DEnter(_onTriggerEnter2D handler)
    {
        onTriggerCallback = handler;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (onTriggerCallback == null) { return; }
        onTriggerCallback(this, collision);
    }
}
