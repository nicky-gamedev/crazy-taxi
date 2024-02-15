using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionPointer : MonoBehaviour
{
    [SerializeField] private MissionManager _mission;

    private void Update()
    {
        transform.LookAt(_mission.transform);
    }
}
