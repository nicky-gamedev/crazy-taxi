using System;
using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class MissionManager : MonoBehaviour
{
    public void Start()
    {
        GetNewDestination();
    }

    public void GetNewDestination()
    {
        var position = new Vector3(Random.Range(0f,240f), 0, Random.Range(0f, 220f));
        NavMeshHit hit;
        while (!NavMesh.SamplePosition(position, out hit, 10, 1))
        {
            position = new Vector3(Random.Range(0f,240f), 0, Random.Range(0f, 220f));
        };
        position = hit.position;
        position.y += 1;
        transform.position = position;
    }

    private void OnTriggerEnter(Collider other)
    {
        GetNewDestination();
    }
}
