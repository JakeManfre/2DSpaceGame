using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damageable : MonoBehaviour
{
    [Header("Properties")]
    [SerializeField]
    float maxHealth;

    [Header("Audio")]
    [SerializeField]
    AudioClip soundOnDeath;

    [SerializeField]
    float soundOnDeathVolume;

    [SerializeField]
    AudioClip soundOnTakeDamage;

    [SerializeField]
    float soundOnTakeDamageVolume;

    float health;

    public void Start()
    {
        health = maxHealth;
    }

    public void takeDamage(float damage)
    {
        if (damage <= 0) { return; }
        health = Mathf.Clamp(health - damage, 0, health);

        if (health == 0)
        {
            Die();
        }
        else
        {
            AudioSource.PlayClipAtPoint(soundOnTakeDamage, Camera.main.transform.position, soundOnTakeDamageVolume);
        }
    }

    public void addHealth(float heal)
    {
        if (heal <= 0) { return; }

        health = Mathf.Clamp(health + heal, health, maxHealth);
    }

    private void Die()
    {
        if (soundOnDeath)
        {
            AudioSource.PlayClipAtPoint(soundOnDeath, Camera.main.transform.position, soundOnDeathVolume);
        }

        Destroy(gameObject);
    }
};
