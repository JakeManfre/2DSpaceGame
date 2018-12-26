using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraZoomController : MonoBehaviour
{
    [SerializeField]
    private bool _zoomCameraOn;
    [SerializeField]
    private CinemachineVirtualCamera _virtualCam;

    [SerializeField]
    private Camera[] _cameras;

    [SerializeField]
    private float _minOrthoSize = 10;
    [SerializeField]
    private float _maxOrthoSize = 15;

    private float _speedPercent;
    private float _orthoPercent;

    public void SetOrthographicSize(float speed, float maxSpeed)
    {
        if (!_zoomCameraOn) return;


        _speedPercent = speed / maxSpeed;

        _speedPercent = 1f - Mathf.Cos(_speedPercent * Mathf.PI * 0.5f);
        //Debug.Log("speed percent: " + _speedPercent);

        _orthoPercent = _minOrthoSize + (_speedPercent *  (_maxOrthoSize - _minOrthoSize));

        _virtualCam.m_Lens.OrthographicSize = _orthoPercent;

        foreach (Camera cam in _cameras)
        {
            cam.orthographicSize = _orthoPercent;
        }
    }
}
