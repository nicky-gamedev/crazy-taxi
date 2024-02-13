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
        NavMesh.SamplePosition(position, out hit, 10, 1);
        transform.position = hit.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("MissionComplete");
    }
}
