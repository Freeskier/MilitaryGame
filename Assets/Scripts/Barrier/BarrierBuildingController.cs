using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class BarrierBuildingController : BuildingController
{
    CinemachineVirtualCamera cinemachine;
    public Transform lookAtTransform;
    public Transform followTransform;
    void Start()
    {
        cinemachine = GameObject.FindGameObjectWithTag("BarrierCamera").GetComponent<CinemachineVirtualCamera>();
        cinemachine.enabled = true;
        cinemachine.m_LookAt = followTransform;
        cinemachine.m_Follow = lookAtTransform;
    }


    
}
