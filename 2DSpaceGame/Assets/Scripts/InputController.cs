using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    public static event EventHandler<InputInfo> LeftInput;
    public static event EventHandler<InputInfo> RightInput;
    public static event EventHandler<InputInfo> UpInput;
    public static event EventHandler<InputInfo> DownInput;
    public static event EventHandler ClearHorizontalInput;
    public static event EventHandler ClearVerticalInput;

    private float horizontal;
    private float vertical;

    private float speed;

    /*---------------------------------------------------------------------------------*/
    // Start is called before the first frame update
    void Start()
    {
        speed = 2; 
    }
    /*---------------------------------------------------------------------------------*/
    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        // move right
        if (horizontal > 0)
        {
            RightInput?.Invoke(this, new InputInfo(horizontal));
        }
        // move left
        if (horizontal < 0)
        { 
            LeftInput?.Invoke(this, new InputInfo(horizontal));
        }
        // left key up
        if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.LeftArrow))
        {
            //Debug.Log("left key up");
            ClearHorizontalInput?.Invoke(this, new EventArgs());
        }
        // right key up
        if (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.RightArrow))
        {
            //Debug.Log("right key up");
            ClearHorizontalInput?.Invoke(this, new EventArgs());
        }


        // move up
        if (vertical > 0)
        {
            UpInput?.Invoke(this, new InputInfo(vertical));
        }
        // move down
        if (vertical < 0)
        {
            DownInput?.Invoke(this, new InputInfo(vertical));
        }
        // up key up
        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.UpArrow))
        {
            //Debug.Log("up key up");
            ClearVerticalInput?.Invoke(this, new EventArgs());
        }
        // down key up
        if (Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.DownArrow))
        {
            //Debug.Log("down key up");
            ClearVerticalInput?.Invoke(this, new EventArgs());
        }
    }
}
/*---------------------------------------------------------------------------------*/
/*---------------- class of input info to pass in event calls ---------------------*/
/*---------------------------------------------------------------------------------*/
public class InputInfo : EventArgs
{
    public float value;

    public InputInfo(float val)
    {
        value = val;
    }
}
