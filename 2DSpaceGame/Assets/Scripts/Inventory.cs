using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [Header("Meta")]
    [SerializeField]
    string name = "";

    [Header("Physical")]
    [SerializeField]
    float sizeCapacity = 0f;

    [SerializeField]
    float weightCapacity = 0f;

    [Header("Sound")]
    [SerializeField]
    AudioClip onInsertSound;

    [SerializeField]
    float onInsertVolume;

    [SerializeField]
    AudioClip onRemoveSound;

    [SerializeField]
    float onRemoveVolume;

    float currSizeTaken = 0f;
    float currWeightTaken = 0f;


    List<Pickup> itemsInInventory;

    // Start is called before the first frame update
    void Start()
    {
        itemsInInventory = new List<Pickup>(); 
    }

    private bool canAdd(Pickup item)
    {
        if (!item) { return false; }
        return ((currSizeTaken      + item.getSize())   <= sizeCapacity) && 
               ((currWeightTaken    + item.getWeight()) <= weightCapacity);
    }


    public bool add(Pickup item)
    {
        if (!item || !canAdd(item)) { return false; }

        // Add to our list
        itemsInInventory.Add(item);

        // Reduce our size
        currSizeTaken   += item.getSize();
        currWeightTaken += item.getWeight();

        return true;
    }

    public bool remove(Pickup item)
    {
        if (!item || !itemsInInventory.Remove(item)) { return false; }

        // Add back in new sizes
        currSizeTaken   -= item.getSize();
        currWeightTaken -= item.getWeight();

        return true;
    }
}
