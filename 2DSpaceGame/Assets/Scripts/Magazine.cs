using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magazine : MonoBehaviour
{
    [Header("Properties")]
    [SerializeField]
    uint clipSize;

    [SerializeField]
    GameObject ammoType;

    private uint numInClip;

    private void Start()
    {
        numInClip = clipSize;
    }

    public bool hasAmmo()
    {
        return true;
        //return numInClip > 0;
    }

    public uint addAmmo(uint amount)
    {
        uint actualAdd = (uint)Mathf.Clamp(amount, 0, clipSize);

        numInClip += actualAdd;

        return amount - actualAdd;
    }

    public void empty()
    {
        numInClip = 0;
    }

    public GameObject getAmmo()
    {
        if (!hasAmmo()) { return null; }
        numInClip -= 1;
        return ammoType;
    }
}
