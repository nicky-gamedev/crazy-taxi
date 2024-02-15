using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GasRefillController : MonoBehaviour
{
    [SerializeField] private int _amountPerSecond;
    private bool _filling;
    private SpaceshipMovement _movement;
    
    private void OnTriggerEnter(Collider other)
    {
        _filling = true;
        _movement = other.GetComponent<SpaceshipMovement>();
        StartCoroutine(FuelRoutine(_movement));
    }

    private void OnTriggerExit(Collider other)
    {
        _filling = false;
    }

    IEnumerator FuelRoutine(SpaceshipMovement movement)
    {
        while (_filling)
        {
            movement.AddFuel(_amountPerSecond);
            yield return new WaitForSeconds(1);
        }
    }
}
