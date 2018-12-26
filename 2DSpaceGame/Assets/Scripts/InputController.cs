using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    public static event EventHandler LeftInput;
    public static event EventHandler RightInput;
    public static event EventHandler UpInput;
    public static event EventHandler DownInput;
    public static event EventHandler ClearHorizontalInput;
    public static event EventHandler ClearVerticalInput;

    /*=========================================================================================*/
    // Update is called once per frame
    void Update()
    {
        /*========================================================================*/
        /*                             Horizontal Input                           */
        /*========================================================================*/
        // move right
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            RightInput?.Invoke(this, new EventArgs());
        }
        // move left
        else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            LeftInput?.Invoke(this, new EventArgs());
        }
        /*-------------------------------------------------------------------------*/
        // left key up
        if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.LeftArrow))
        {
            ClearHorizontalInput?.Invoke(this, new EventArgs());
        }
        // right key up
        else if (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.RightArrow))
        {
            ClearHorizontalInput?.Invoke(this, new EventArgs());
        }
        /*========================================================================*/
        /*                              Vertical Input                            */
        /*========================================================================*/
        // move up
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            UpInput?.Invoke(this, new EventArgs());
        }
        // move down
        else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            DownInput?.Invoke(this, new EventArgs());
        }
        /*-------------------------------------------------------------------------*/
        // up key up
        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.UpArrow))
        {
            //Debug.Log("up key up");
            ClearVerticalInput?.Invoke(this, new EventArgs());
        }
        // down key up
        else if (Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.DownArrow))
        {
            //Debug.Log("down key up");
            ClearVerticalInput?.Invoke(this, new EventArgs());
        }
    }
}
/*=================================================================================*/
/*                   class of input info to pass in event calls                    */
/*=================================================================================*/
//public class InputInfo : EventArgs
//{
//    public float value;

//    public InputInfo(float val)
//    {
//        value = val;
//    }
//}
