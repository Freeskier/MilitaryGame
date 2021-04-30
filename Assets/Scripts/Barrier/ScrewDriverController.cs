using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrewDriverController : MonoBehaviour
{
    public GameObject screwDriverPrefab;
    public BuildingController buildingController;
    public Transform camTransform;
    private GameObject screwDriverObject;
    
    void Start()
    {
        
    }

    void Update()
    {
        if(screwDriverObject == null && buildingController.baseBuildingFinished)
        {
            screwDriverObject = Instantiate(screwDriverPrefab, camTransform.position, Quaternion.identity);
            
        }
    }
}
