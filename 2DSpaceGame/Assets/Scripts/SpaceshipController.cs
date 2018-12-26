using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceshipController : MonoBehaviour
{
    [SerializeField]
    private Camera _mainCamera;
    [SerializeField]
    private Camera[] _extraCameras;
    private Vector3 _camPos;

    [SerializeField]
    private float _acceleration = 2;
    [SerializeField]
    private bool _showShipsVector;

    [SerializeField]
    private float _maxSpeed;
    private int _horizontalForce;
    private float _currentSpeed;

    private int _verticalForce;

    private Vector3 _rotation;
    private Vector3 _shipsForwardPoint;
    private Vector2 _velocity;

    private LineRenderer _line;

    const float EPSILON = 0.05f;


    /*=========================================================================================*/
    /// <summary>
    /// Start is called before the first frame update
    /// </summary>
    void Start()
    {
        InputController.RightInput += InputController_RightInput;
        InputController.LeftInput += InputController_LeftInput;
        InputController.UpInput += InputController_UpInput;
        InputController.DownInput += InputController_DownInput;
        InputController.ClearHorizontalInput += InputController_ClearHorizontalInput;
        InputController.ClearVerticalInput += InputController_ClearVerticalInput;

        _camPos = _mainCamera.transform.position;
        _rotation = Vector3.zero;
        _shipsForwardPoint = transform.position + transform.up;

        _line = GetComponent<LineRenderer>();
        _line.enabled = _showShipsVector;

        gameObject.transform.GetChild(0).eulerAngles = new Vector3(90, 0, 0);
        transform.LookAt(_shipsForwardPoint);
    }
    /*=========================================================================================*/
    /// <summary>
    /// updates every frame
    /// </summary>
    private void Update()
    {

    }
    /*=========================================================================================*/
    /// <summary>
    /// updates for physics at constant rate
    /// </summary>
    private void FixedUpdate()
    {
        //TODO: normalize velocity
        _currentSpeed = Mathf.Abs(GetComponent<Rigidbody2D>().velocity.magnitude);
        //Debug.Log("speed: " + _currentSpeed);

        // add horizontal forces
        if (_horizontalForce != 0 && _currentSpeed < _maxSpeed)
        {
            GetComponent<Rigidbody2D>().AddForce(new Vector2(_horizontalForce * _acceleration, 0));
        }

        // add vertical forces
        if (_verticalForce != 0 && _currentSpeed < _maxSpeed)
        {
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0, _verticalForce * _acceleration));
        }

        //GetComponent<Rigidbody2D>().velocity.Normalize();
        //_currentSpeed = Mathf.Abs(GetComponent<Rigidbody2D>().velocity.magnitude);
        //Debug.Log("speed: " + _currentSpeed);

        // if the ship is not moving,
        // return so the cameras pos and ships rot don't continue to update
        if (Math.Abs(GetComponent<Rigidbody2D>().velocity.magnitude) < EPSILON)
        {
            return;
        }

        _camPos = _mainCamera.transform.position;

        // set each cameras position to the main camera position.
        // the main camera position is controlled by a cinamachine virtual camera.
        foreach (Camera cam in _extraCameras)
        {
            cam.transform.position = _camPos;
        }

        // get the ships velocity components and set the ships forward to that point
        _velocity = GetComponent<Rigidbody2D>().velocity;
        _shipsForwardPoint = transform.position + new Vector3(_velocity.x, _velocity.y, 0);

        // if _showShipsVector bool is checked in the inspector, show the vector visually via line renderer
        if (_showShipsVector)
        {
            _line.SetPosition(0, transform.position);
            _line.SetPosition(1, _shipsForwardPoint);
        }

        // have the ships forward look at the the direction it's moving
        transform.LookAt(_shipsForwardPoint, transform.up);

    }
    /*=========================================================================================*/
    /// <summary>
    /// Inputs the controller right input.
    /// </summary>
    /// <param name="sender">Sender.</param>
    /// <param name="e">E.</param>
    private void InputController_RightInput(object sender, EventArgs e)
    {
        _horizontalForce = 1;
    }
    /*=========================================================================================*/
    /// <summary>
    /// Inputs the controller left input.
    /// </summary>
    /// <param name="sender">Sender.</param>
    /// <param name="e">E.</param>
    private void InputController_LeftInput(object sender, EventArgs e)
    {
        _horizontalForce = -1;
    }
    /*=========================================================================================*/
    /// <summary>
    /// Inputs the controller clear horizontal input.
    /// </summary>
    /// <param name="sender">Sender.</param>
    /// <param name="e">E.</param>
    private void InputController_ClearHorizontalInput(object sender, EventArgs e)
    {
        _horizontalForce = 0;
    }
    /*=========================================================================================*/
    /// <summary>
    /// Inputs the controller up input.
    /// </summary>
    /// <param name="sender">Sender.</param>
    /// <param name="e">E.</param>
    private void InputController_UpInput(object sender, EventArgs e)
    {
        _verticalForce = 1;
    }
    /*=========================================================================================*/
    /// <summary>
    /// Inputs the controller down input.
    /// </summary>
    /// <param name="sender">Sender.</param>
    /// <param name="e">E.</param>
    private void InputController_DownInput(object sender, EventArgs e)
    {
        _verticalForce = -1;
    }
    /*=========================================================================================*/
    /// <summary>
    /// Inputs the controller clear vertical input.
    /// </summary>
    /// <param name="sender">Sender.</param>
    /// <param name="e">E.</param>
    private void InputController_ClearVerticalInput(object sender, EventArgs e)
    {
        _verticalForce = 0;
    }
    /*=========================================================================================*/
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
