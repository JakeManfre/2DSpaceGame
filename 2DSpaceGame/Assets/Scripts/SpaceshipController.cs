using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceshipController : MonoBehaviour
{
    [SerializeField]
    private Camera[] _cameras;
    private Vector3 _playerCamPos;

    [SerializeField]
    private float _speed = 2;

    [SerializeField]
    private bool _showShipsVector;

    private float _horizontalMag;
    private float _verticalMag;

    private Vector3 _rotation;

    private LineRenderer _line;

    private Vector3 _shipsForwardPoint;

    /*---------------------------------------------------------------------------------*/
    // Start is called before the first frame update
    void Start()
    {
        InputController.RightInput += InputController_RightInput;
        InputController.LeftInput += InputController_LeftInput;
        InputController.UpInput += InputController_UpInput;
        InputController.DownInput += InputController_DownInput;
        InputController.ClearHorizontalInput += InputController_ClearHorizontalInput;
        InputController.ClearVerticalInput += InputController_ClearVerticalInput;

        _playerCamPos = _cameras[0].transform.position;
        _rotation = Vector3.zero;
        _shipsForwardPoint = transform.position + transform.up;

        _line = GetComponent<LineRenderer>();
        _line.enabled = _showShipsVector;

        gameObject.transform.GetChild(0).eulerAngles = new Vector3(90, 0, 0);
        transform.LookAt(_shipsForwardPoint);
    }
    /*---------------------------------------------------------------------------------*/
    // updates every frame
    private void Update()
    {
        // set camera position to follow the players ship
        _playerCamPos.x = transform.position.x;
        _playerCamPos.y = transform.position.y;

        // set each cameras position to the ships position
        foreach (Camera cam in _cameras)
        {
            cam.transform.position = _playerCamPos;
        }

        Vector2 velocity = GetComponent<Rigidbody2D>().velocity;

        _shipsForwardPoint = transform.position + new Vector3(velocity.x, velocity.y, 0);

        // if show ships vector bool is checked in the inspector, show the vector visually
        if (_showShipsVector)
        {
            _line.SetPosition(0, transform.position);
            _line.SetPosition(1, _shipsForwardPoint);
        }

        transform.LookAt(_shipsForwardPoint, transform.up);
    }
    /*---------------------------------------------------------------------------------*/
    /// <summary>
    /// Inputs the controller right input.
    /// </summary>
    /// <param name="sender">Sender.</param>
    /// <param name="e">E.</param>
    private void InputController_RightInput(object sender, InputInfo e)
    {
        gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(_speed, 0));
    }
    /*---------------------------------------------------------------------------------*/
    /// <summary>
    /// Inputs the controller left input.
    /// </summary>
    /// <param name="sender">Sender.</param>
    /// <param name="e">E.</param>
    private void InputController_LeftInput(object sender, InputInfo e)
    {
        gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(-_speed, 0));
    }
    /*---------------------------------------------------------------------------------*/
    /// <summary>
    /// Inputs the controller clear horizontal input.
    /// </summary>
    /// <param name="sender">Sender.</param>
    /// <param name="e">E.</param>
    private void InputController_ClearHorizontalInput(object sender, EventArgs e)
    {
        _verticalMag = 0;
    }
    /*---------------------------------------------------------------------------------*/
    /// <summary>
    /// Inputs the controller up input.
    /// </summary>
    /// <param name="sender">Sender.</param>
    /// <param name="e">E.</param>
    private void InputController_UpInput(object sender, InputInfo e)
    {
        gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, _speed));
    }
    /*---------------------------------------------------------------------------------*/
    /// <summary>
    /// Inputs the controller down input.
    /// </summary>
    /// <param name="sender">Sender.</param>
    /// <param name="e">E.</param>
    private void InputController_DownInput(object sender, InputInfo e)
    {
        gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, -_speed));
    }
    /*---------------------------------------------------------------------------------*/
    /// <summary>
    /// Inputs the controller clear vertical input.
    /// </summary>
    /// <param name="sender">Sender.</param>
    /// <param name="e">E.</param>
    private void InputController_ClearVerticalInput(object sender, EventArgs e)
    {
        _horizontalMag = 0;
    }
    /*---------------------------------------------------------------------------------*/
    /// <summary>
    /// Unsubscribe methods from events on destroy.
    /// </summary>
    private void OnDestroy()
    {
        InputController.RightInput -= InputController_RightInput;
        InputController.LeftInput -= InputController_LeftInput;
        InputController.UpInput -= InputController_UpInput;
        InputController.DownInput -= InputController_DownInput;
        InputController.ClearHorizontalInput -= InputController_ClearHorizontalInput;
        InputController.ClearVerticalInput -= InputController_ClearVerticalInput;
    }
}
