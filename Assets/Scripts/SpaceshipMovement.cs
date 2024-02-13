using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class SpaceshipMovement : MonoBehaviour
{
    [SerializeField] private InputReader _inputReader;
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private Camera _mainCamera;
    [SerializeField] private float _speed;
    [SerializeField] private float _turnRate;
    [SerializeField] private float _brakeFactor;

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
    
    private void ProcessMovement(Vector2 movementInput)
    {
        _rb.AddForce(transform.forward * movementInput.y * _speed, ForceMode.Acceleration);
        Vector3 eu = _mainCamera.transform.rotation.eulerAngles;
        transform.rotation = Quaternion.RotateTowards(
            transform.rotation,
            Quaternion.Euler(eu.x, eu.y, 0.0f),
            _turnRate * Time.deltaTime);
    }
    
    private void DoHandbreak()
    {
        _rb.AddForce(-_brakeFactor * _rb.velocity);
       
        if (_rb.velocity.sqrMagnitude < 0.1) {
            _rb.Sleep();
        }
    }
    

}
