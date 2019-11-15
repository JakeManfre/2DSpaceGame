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

    private void Start()
    {
        foreach (Gun gun in gunList)
        {
            gun.Initialize(gameObject);
        }

        guns = new Cycleable<Gun>();
        guns.SetList(gunList);
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
            guns.GetSelected().pullTrigger();
        }
        else if (Input.GetButtonUp("Fire"))
        {
            guns.GetSelected().releaseTrigger();
        }
        else if (Input.GetButtonDown("FireMode"))
        {
            guns.GetSelected().changeMode();
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
}
