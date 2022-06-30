using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Camera))]
public class CameraFollower : MonoBehaviour
{
    [SerializeField] private Player _target;
    [SerializeField] private float _offSet;
    [SerializeField] private Transform _rightBorder;
    [SerializeField] private Transform _leftBorder;

    private Camera _camera;
    private Vector3 TargetPosition => _target.transform.position;

    private void Awake()
    {
        _camera = GetComponent<Camera>();
        
        _offSet = _camera.orthographicSize * _camera.aspect;
    }

    private void Update()
    {
        Follow();
    }

    private void Follow()
    {
        if(Math.Abs(transform.position.x - TargetPosition.x) == 0) return;

        if(TargetPosition.x <= _leftBorder.position.x + _offSet) return;
        if(TargetPosition.x >= _rightBorder.position.x - _offSet) return;

        transform.position = new Vector3(TargetPosition.x, transform.position.y, transform.position.z);
    }
}
