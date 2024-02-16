using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.XR;

public class SpaceshipMovement : MonoBehaviour
{
    [SerializeField] private InputReader _inputReader;
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private Camera _mainCamera;
    [SerializeField] private float _speed;
    [SerializeField] private float _turnRate;
    [SerializeField] private float _brakeFactor;
    public int fuel;
    public int maxFuel;

    private bool _consumingFuel;
    private float _timer;

    public Vector3 Velocity => _rb.velocity;

    private void OnEnable()
    {
        _inputReader.MovementEvent += ProcessMovement;
        _inputReader.HandbrakeEvent += DoHandbreak;
    }

    private void OnDisable()
    {
        _inputReader.MovementEvent -= ProcessMovement;
        _inputReader.HandbrakeEvent -= DoHandbreak;
    }

    private void FixedUpdate()
    {
        _timer += Time.fixedDeltaTime;
        if (_consumingFuel && _timer >= 1f && fuel > 0)
        {
            fuel = (int)Mathf.Clamp(fuel - 1, 0, Mathf.Infinity);
            if (fuel == 0)
            {
                GameManager.Instance.GameOver();
            }
            _timer = 0;
        }
        
    }

    private void ProcessMovement(Vector2 movementInput)
    {
        if (fuel > 0)
        {
            _rb.AddForce(transform.forward * movementInput.y * _speed, ForceMode.Acceleration);
        }

        Vector3 eu = _mainCamera.transform.rotation.eulerAngles;
        transform.rotation = Quaternion.RotateTowards(
            transform.rotation,
            Quaternion.Euler(eu.x, eu.y, 0.0f),
            _turnRate * Time.deltaTime);

        _consumingFuel = movementInput.y != 0;
    }
    
    private void DoHandbreak()
    {
        _rb.AddForce(-_brakeFactor * _rb.velocity);
       
        if (_rb.velocity.sqrMagnitude < 0.1) {
            _rb.Sleep();
        }
    }

    public void AddFuel(int amount) => fuel += amount;
}
