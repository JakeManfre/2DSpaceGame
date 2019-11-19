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
    float burstFireRate = .02f;

    [SerializeField]
    float autoFireRate  = .3f;

    [SerializeField]
    uint shotsPerBurst  = 4;

    [SerializeField]
    float shotCooldown  = .1f;

    [SerializeField]
    float launchSpeed   = 15;

    [Header("Fire Mode")]
    [SerializeField]
    List<FIRE_MODE> availableModes;

    Cycleable<FIRE_MODE> modes;

    // Tracking states
    bool isFiring;
    bool triggerPulled;
    float lastTriggerPull;


    // Helpers
    WeaponSystem parentSystem;

    protected abstract void Projectile_OnTriggerEnter2D(Projectile projectile, Collider2D collider);

    public void Start()
    {
        modes = new Cycleable<FIRE_MODE>();
        modes.SetList(availableModes);

        isFiring        = false;
        triggerPulled   = false;
        lastTriggerPull = 0;
    }

    public void Initialize(WeaponSystem parent)
    {
        parentSystem = parent;
    }

    public void PullTrigger()
    {
        if (isFiring) { return; }

        // See if cooldown has elapsed
        if ((Time.time - lastTriggerPull) < shotCooldown) { return; }

        lastTriggerPull = Time.time;
        triggerPulled   = true;

        switch(GetMode())
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

    public void ReleaseTrigger()
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
        if (!magazine.HasAmmo())
        {
            AudioSource.PlayClipAtPoint(onEmptyFire, Camera.main.transform.position, onEmptyFireVolume);
            return false;
        }

        // TODO Figure out why -90 worked
        float zAngle = Mathf.Atan2(transform.forward.y, transform.forward.x) * Mathf.Rad2Deg - 90;

        GameObject newGameObject = Instantiate(magazine.GetAmmo(), transform.position, Quaternion.AngleAxis(zAngle, Vector3.forward)) as GameObject;

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
        Vector2 parentVelocity = parentSystem.GetRigidBodyVelocity();

        rigidBody2D.velocity = (transform.forward * launchSpeed) + new Vector3(parentVelocity.x, parentVelocity.y);

        AudioSource.PlayClipAtPoint(onFire, Camera.main.transform.position, onFireVolume);

        return true;
    }

    public void Reload(uint amount)
    {
        // We are firing
        if (isFiring) { return; }
        magazine.AddAmmo(amount);
    }

    public void ChangeMode()
    {
        modes.ChooseNext();
    }

    public FIRE_MODE GetMode()
    {
        return modes.GetSelected();
    }
}
