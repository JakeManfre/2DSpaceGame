using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    [Header("Meta")]
    [SerializeField]
    string name = "";

    [Header("Physical")]
    [SerializeField]
    float size = 0f;

    [SerializeField]
    float weight = 0f;

    [Header("Particle")]
    [SerializeField]
    GameObject pickupEffect;

    [Header("Sound")]
    [SerializeField]
    AudioClip pickupSound;

    [SerializeField]
    float pickupVolume;

    [SerializeField]
    AudioClip dropSound;

    [SerializeField]
    float dropVolume;

    public string getName()
    {
        return name;
    }

    public float getSize()
    {
        return size;
    }

    public float getWeight()
    {
        return weight;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Get the game object
        Inventory inventory = collision.GetComponent<Inventory>();

        // Item can't be picked up by collider
        if (!inventory) { return;  }

        InventoryManager.AddToInventory(inventory, this);
    }

    
    public void soundOnPickUp()
    {
        playSound(pickupSound, pickupVolume);
    }

    public void soundOnDrop()
    {
        playSound(dropSound, dropVolume);
    }

    private void playSound(AudioClip sound, float volume)
    {
        if (!sound) { return; }
        AudioSource.PlayClipAtPoint(sound, Camera.main.transform.position, volume);
    }
}
