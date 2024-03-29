using System;
using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class MissionManager : MonoBehaviour
{
    [SerializeField] private int _addTime;
    public void GetNewDestination()
    {
        var position = new Vector3(Random.Range(-163f,436f), 0, Random.Range(-180f, 310f));
        NavMeshHit hit;
        while (!NavMesh.SamplePosition(position, out hit, 10, 1))
        {
            position = new Vector3(Random.Range(0f,240f), 0, Random.Range(0f, 220f));
        };
        position = hit.position;
        position.y += 5.5f;
        transform.position = position;
    }

    private void OnTriggerEnter(Collider other)
    {
        GetNewDestination();
        GameManager.Instance.AddTime(_addTime);
    }
}
