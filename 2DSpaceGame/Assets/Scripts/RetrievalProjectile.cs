using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RetrievalProjectile : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 10);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject collidedGameObject = collision.gameObject;
        if (collidedGameObject == null) { return; }

        Pickup pickupComponent = collidedGameObject.GetComponent<Pickup>();
        if (pickupComponent == null) { return; }

        Inventory inventory = gameObject.GetComponentInParent<Inventory>();
        if (inventory == null) { return; }

        InventoryManager.addToInventory(inventory, pickupComponent);
    }
}
