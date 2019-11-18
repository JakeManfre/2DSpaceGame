﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager
{
    public static bool addToInventory(Inventory inventory, Pickup item)
    {
        // Bad data
        if (!inventory || !item) { return false; }

        // Can't fit
        if (!inventory.add(item)) { return false; }

        // Do things to objects
        item.soundOnPickUp();
        item.gameObject.SetActive(false);

        return true;
    }
    public static bool removeFromInventory(Inventory inventory, Pickup item)
    {
        // Bad data
        if (!inventory || !item) { return false; }

        // Can't fit
        if (!inventory.remove(item)) { return false; }

        // Do things to objects
        item.soundOnDrop();
        item.gameObject.SetActive(true);
        return true;
    }

    public static bool tryAddToInventory(GameObject shouldHaveInventory, GameObject shouldBePickable)
    {
        // Get the Pickup component
        Pickup pickupComponent = shouldBePickable.GetComponent<Pickup>();
        if (pickupComponent == null) { return false; }

        Inventory inventory = shouldHaveInventory.GetComponentInParent<Inventory>();
        if (inventory == null) { return false; }

        // Add it to the inventory
        return addToInventory(inventory, pickupComponent);
    }
}
