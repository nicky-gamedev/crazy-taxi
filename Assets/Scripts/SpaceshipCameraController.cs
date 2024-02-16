using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class SpaceshipCameraController : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private InputReader _inputReader;
    [SerializeField] private SpaceshipMovement _movement;
    [SerializeField] private float _horizontalDistance;
    [SerializeField] private float _verticalDistance;
    
    [SerializeField] private float _minimumRotation;
    [SerializeField] private float _maximumRotation;
    [SerializeField] private float _horizontalSpeed;
    [SerializeField] private float _verticalSpeed;

    private Vector3 _currentLookDirection;
    private Vector2 _inputLookDirection;

    private void OnEnable()
    {
        _inputReader.LookEvent += LookUpdate;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
    }

    private void Awake()
    {
        _currentLookDirection = transform.eulerAngles;
    }

    private void OnDisable()
    {
        _inputReader.LookEvent -= LookUpdate;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    private void Update()
    {
        var final = _movement.transform.position - (_movement.transform.forward * _horizontalDistance);
        final.y += _verticalDistance;
        transform.position = final;
        UpdateCameraFOV();
    }

    private void LateUpdate()
    {
        _currentLookDirection.x += _inputLookDirection.x * _verticalSpeed* Time.deltaTime;
        _currentLookDirection.y += _inputLookDirection.y * _horizontalSpeed * Time.deltaTime;
        _currentLookDirection.y = Mathf.Clamp(_currentLookDirection.y, _minimumRotation, _maximumRotation);
        transform.localRotation = Quaternion.Euler(-_currentLookDirection.y, _currentLookDirection.x, 0f);
    }

    void LookUpdate(Vector2 look)
    {
        _inputLookDirection.y = look.y * Time.deltaTime;
        _inputLookDirection.x = look.x * Time.deltaTime;
    }

    private void UpdateCameraFOV()
    {
        float target = Mathf.Abs(_movement.Velocity.sqrMagnitude) > 10 ? 110 : 90;
        _camera.fieldOfView = Mathf.Lerp(_camera.fieldOfView, target, 1 * Time.deltaTime);
    }
}
