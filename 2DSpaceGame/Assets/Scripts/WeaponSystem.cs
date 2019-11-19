using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WeaponSystem : MonoBehaviour
{
    [Header("Meta")]
    [SerializeField]
    string name;

    [Header("GunSlots")]
    [SerializeField]
    List<Gun> gunList;

    Cycleable<Gun> guns;

    [SerializeField]
    Transform firePoint;

    private void Start()
    {
        guns = new Cycleable<Gun>();

        foreach (Gun gunPrefab in gunList)
        {
            Gun gun = Instantiate(gunPrefab, firePoint);
            gun.Initialize(this);
            guns.AddItem(gun);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("SwitchWeapon"))
        {
            ChangeWeapon();
        }
        else if (Input.GetButtonDown("Fire"))
        {
            guns.GetSelected().PullTrigger();
        }
        else if (Input.GetButtonUp("Fire"))
        {
            guns.GetSelected().ReleaseTrigger();
        }
        else if (Input.GetButtonDown("FireMode"))
        {
            guns.GetSelected().ChangeMode();
        }
    }

    public void ChangeWeapon()
    {
        guns.ChooseNext();
    }

    public List<Gun> GetGuns()
    {
        return gunList;
    }

    public Vector2 GetRigidBodyVelocity()
    {
        Rigidbody2D parentRigidBody2D = gameObject.GetComponent<Rigidbody2D>();
        if (!parentRigidBody2D) { return Vector3.zero; }

        return parentRigidBody2D.velocity;
    }
}
