using System;
using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    [SerializeField] private float _parallaxPower;
    
    private Camera _camera;
    private float _previousCameraXPosition;

    public Vector3 CurrentPosition => transform.position;

    private void Start()
    {
        _camera = Camera.main;
        _previousCameraXPosition = _camera.transform.position.x;
    }

    private void Update()
    {
        if(Math.Abs(_camera.transform.position.x - _previousCameraXPosition) == 0) return;

        var distance = _previousCameraXPosition - _camera.transform.position.x;

        transform.position = new Vector3(CurrentPosition.x + distance * _parallaxPower, CurrentPosition.y,
            CurrentPosition.z);
        
        _previousCameraXPosition = _camera.transform.position.x;
    }
}
