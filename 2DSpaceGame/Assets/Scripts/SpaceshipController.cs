using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceshipController : MonoBehaviour
{
    [SerializeField]
    private readonly InputController _inputController;
    [SerializeField]
    float speed = 2;

    // Start is called before the first frame update
    void Start()
    {
        InputController.HorizontalInput += InputController_HorizontalInput;
        InputController.VerticalInput += InputController_VerticalInput;
    }

    /// <summary>
    /// Called when horizontal input event is called
    /// </summary>
    /// <param name="sender">Sender.</param>
    /// <param name="e">E.</param>
    private void InputController_HorizontalInput(object sender, InputInfo e)
    {
        // right input
        if (e.value > 0)
        {
            gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(speed, 0));
        }
        // left input
        else if (e.value < 0)
        {
            gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(-speed, 0));
        }
    }

    /// <summary>
    /// Called when vertical input event is called
    /// </summary>
    /// <param name="sender">Sender.</param>
    /// <param name="e">E.</param>
    private void InputController_VerticalInput(object sender, InputInfo e)
    {
        // up input
        if (e.value > 0)
        {
            gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, speed));
        }
        // down input
        else if (e.value < 0)
        {
            gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, -speed));
        }
    }
}
