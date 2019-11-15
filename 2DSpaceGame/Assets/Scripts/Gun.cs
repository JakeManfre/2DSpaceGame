using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
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

    // Curent mode of the gun, look above
    int currentMode;

    // Tracking states
    private bool isFiring       = false;
    private bool triggerPulled  = false;
    float lastTriggerPull = 0;

    protected void Update()
    {
        if (Input.GetButtonDown("Fire"))
        {
            pullTrigger();
        }
        else if (Input.GetButtonUp("Fire"))
        {
            releaseTrigger();
        }
        else if (Input.GetButtonDown("FireMode"))
        {
            changeMode();
        }
    }

    public void pullTrigger()
    {
        if (isFiring) { return; }

        // See if cooldown has elapsed
        if ((Time.time - lastTriggerPull) < shotCooldown) { return; }

        lastTriggerPull = Time.time;
        triggerPulled   = true;

        switch(availableModes[currentMode])
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

        GameObject projectile = Instantiate(magazine.getAmmoType(), transform.position, Quaternion.identity) as GameObject;
        projectile.transform.SetParent(gameObject.transform);

        Rigidbody2D rigidBody2DComponent = projectile.GetComponent<Rigidbody2D>();
        if (!rigidBody2DComponent)
        {
            Destroy(projectile);
            return false;
        }

        rigidBody2DComponent.velocity = new Vector2(0, launchSpeed);

        magazine.decrementAmmo();

        AudioSource.PlayClipAtPoint(onFire, Camera.main.transform.position, onFireVolume);

        return true;
    }

    public void reload(uint amount)
    {
        // We are firing
        if (isFiring) { return; }
        magazine.addAmmo(amount);
    }

    public bool changeMode()
    {
        if (isFiring) { return false; }

        ++currentMode;

        //wrap the mode to the front of the list
        if (currentMode >= availableModes.Count) 
        { 
            currentMode = 0; 
        }

        return true;
    }

    public FIRE_MODE getMode()
    {
        return availableModes[currentMode];
    }
}
