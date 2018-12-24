using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    //public static InputController Instance { set; get; }

    public static event EventHandler<InputInfo> HorizontalInput;
    public static event EventHandler<InputInfo> VerticalInput;

    float horizontal;
    float vertical;

    float speed;

    // Start is called before the first frame update
    void Start()
    {
        //Instance = this;
        speed = 2; 
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        // move right
        if (horizontal > 0)
        {
            HorizontalInput?.Invoke(this, new InputInfo(horizontal));
        }
        // move left
        if (horizontal < 0)
        { 
        HorizontalInput?.Invoke(this, new InputInfo(horizontal));
        }
        // move up
        if (vertical > 0)
        {
            VerticalInput?.Invoke(this, new InputInfo(vertical));
        }
        // move down
        if (vertical < 0)
        {
            VerticalInput?.Invoke(this, new InputInfo(vertical));
        }
    }
}

public class InputInfo : EventArgs
{
    public float value;

    public InputInfo(float val)
    {
        value = val;
    }
}
