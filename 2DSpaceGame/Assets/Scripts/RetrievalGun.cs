using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RetrievalGun : Gun
{
    Inventory inventoryToSendTo = null;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SetInventory(Inventory inventory)
    {
        inventoryToSendTo = inventory;
    }

    protected override void Projectile_OnTriggerEnter2D(Projectile projectile, Collider2D collider)
    {
        // Gun doesn't have an inventory
        if (inventoryToSendTo == null) { return; }

        // Get the Pickup component
        Pickup pickupComponent = collider.GetComponent<Pickup>();
        if (pickupComponent == null) { return; }

        // Add it to the inventory
        InventoryManager.addToInventory(inventoryToSendTo, pickupComponent);
    }
}
