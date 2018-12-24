using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    [SerializeField]
    private Transform _playerShip;
    [SerializeField]
    [Header("Parallax rate must be greater than 0.")]
    private int _parallaxRate;

    private Vector2 _initialOffset;

    private float _horizontalParallax;
    private float _verticalParallax;

    Material _starfieldMat;
    string _texture;

    float _screenWidth;
    const float _backgroundBuffer = 1.2f;

    // Start is called before the first frame update
    void Start()
    {
        if (_parallaxRate == 0)
            Debug.LogError("Parallax rate must be greater than 0!");

        _starfieldMat = GetComponent<Renderer>().material;
        _texture = "_MainTex";

        _initialOffset = _starfieldMat.GetTextureOffset(_texture);

        _screenWidth = (Camera.main.orthographicSize * Screen.width / Screen.height) * 2;

        transform.localScale = new Vector2(_screenWidth * _backgroundBuffer, _screenWidth * _backgroundBuffer);
    }

    // Update is called once per frame
    void Update()
    {
        // set texture position to ships positoin
        transform.position = _playerShip.position;

        // calculate and set texture offset based on the ships position
        _horizontalParallax = _playerShip.position.x / _parallaxRate;
        _verticalParallax = _playerShip.position.y / _parallaxRate;

        _starfieldMat.SetTextureOffset(_texture, _initialOffset + new Vector2(_horizontalParallax, _verticalParallax));
    }
}
