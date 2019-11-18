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

    public bool HasAmmo()
    {
        return true;
        //return numInClip > 0;
    }

    public uint AddAmmo(uint amount)
    {
        uint actualAdd = (uint)Mathf.Clamp(amount, 0, clipSize);

        numInClip += actualAdd;

        return amount - actualAdd;
    }

    public void Empty()
    {
        numInClip = 0;
    }

    public GameObject GetAmmo()
    {
        if (!HasAmmo()) { return null; }
        numInClip -= 1;
        return ammoType;
    }
}
