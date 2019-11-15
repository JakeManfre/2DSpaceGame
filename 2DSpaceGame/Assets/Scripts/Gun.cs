using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Gun : MonoBehaviour
{
    public enum FIRE_MODE { SINGLE, BURST, AUTO }

    [Header("Meta")]
    [SerializeField]
    string name = "";

    [Header("Magazine")]
    [SerializeField]
    Magazine magazine;

    [Header("Sound")]
    [SerializeField]
    AudioClip onFire;

    [SerializeField]
    float onFireVolume;

    [SerializeField]
    AudioClip onEmptyFire;

    [SerializeField]
    float onEmptyFireVolume;

    [SerializeField]
    AudioClip onReload;

    [SerializeField]
    float onReloadVolume;

    [Header("Properties")]
    [SerializeField]
    float burstFireRate;

    [SerializeField]
    float autoFireRate;

    [SerializeField]
    uint shotsPerBurst;

    [SerializeField]
    float shotCooldown;

    [SerializeField]
    float launchSpeed;

    [Header("Fire Mode")]
    [SerializeField]
    List<FIRE_MODE> availableModes;

    Cycleable<FIRE_MODE> modes;

    // Tracking states
    bool isFiring;
    bool triggerPulled;
    float lastTriggerPull;

    protected GameObject myOwner;

    protected abstract void Projectile_OnTriggerEnter2D(Projectile projectile, Collider2D collider);

    public void Initialize(GameObject owner)
    {
        modes = new Cycleable<FIRE_MODE>();
        modes.SetList(availableModes);

        isFiring = false;
        triggerPulled = false;
        lastTriggerPull = 0;

        myOwner = owner; 
    }

    public void pullTrigger()
    {
        if (isFiring) { return; }

        // See if cooldown has elapsed
        if ((Time.time - lastTriggerPull) < shotCooldown) { return; }

        lastTriggerPull = Time.time;
        triggerPulled   = true;

        switch(getMode())
        {
            case FIRE_MODE.SINGLE:
                Fire_Single();
                break;
            case FIRE_MODE.BURST:
                StartCoroutine(Fire_Burst());
                break;
            case FIRE_MODE.AUTO:
                StartCoroutine(Fire_Auto());
                break;
        }
    }

    public void releaseTrigger()
    {
        triggerPulled = false;
    }

    private void Fire_Single()
    {
        Fire_Projectile();
    }

    private IEnumerator Fire_Burst()
    {
        // Only used in burst mode
        uint burstShotsLeft = shotsPerBurst;

        bool canFire = true;

        isFiring = true;

        // Fire goes while there is ammo, until shots on this pull are done unless the mode is auto
        while (canFire && burstShotsLeft > 0)
        {
            canFire = Fire_Projectile();
            --burstShotsLeft;
            yield return new WaitForSeconds(burstFireRate);
        }

        isFiring = false;
    }

    private IEnumerator Fire_Auto()
    {
        bool canFire = true;

        isFiring = true;

        while (canFire && triggerPulled)
        {
            canFire = Fire_Projectile();
            yield return new WaitForSeconds(autoFireRate);
        }

        isFiring = false;
    }

    private bool Fire_Projectile()
    {
        if (!magazine.hasAmmo())
        {
            AudioSource.PlayClipAtPoint(onEmptyFire, Camera.main.transform.position, onEmptyFireVolume);
            return false;
        }

        GameObject newGameObject = Instantiate(magazine.getAmmo(), transform.position, Quaternion.identity) as GameObject;

        Projectile projectile   = newGameObject.GetComponent<Projectile>();
        Rigidbody2D rigidBody2D = newGameObject.GetComponent<Rigidbody2D>();

        if (!rigidBody2D || !projectile)
        {
            Destroy(projectile);
            return false;
        }

        // Register the event
        projectile.OnTriggerEnter2D_Event += this.Projectile_OnTriggerEnter2D;

        // Set the velocity
        rigidBody2D.velocity = new Vector2(0, launchSpeed);

        AudioSource.PlayClipAtPoint(onFire, Camera.main.transform.position, onFireVolume);

        return true;
    }

    public void reload(uint amount)
    {
        // We are firing
        if (isFiring) { return; }
        magazine.addAmmo(amount);
    }

    public void changeMode()
    {
        modes.ChooseNext();
    }

    public FIRE_MODE getMode()
    {
        return modes.GetSelected();
    }
}
