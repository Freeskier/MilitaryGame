using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BuildingController : MonoBehaviour
{
    public GameObject diggingArea;
    public GameObject builtObject;
    public float diggingHeight = 0f;
    public float rotationMultiplier = 15f;
    private PlayerControl playerControl;
    protected GameObject InstantiatedPlace;
    protected bool hasPlacingItemFinished = true;
    public LayerMask layerMask;    
    [System.NonSerialized] public float RotationInput;
    [System.NonSerialized] public bool baseBuildingFinished = false;


    public virtual void Update()
    {
        if(baseBuildingFinished)
        {
            GameManager.instance.state = GameManager.State.nothing;
            builtObject.GetComponent<BrickController>().enabled = false;
            this.enabled = false;
        }

        MoveSpawnedObject();
    }

    public virtual void MoveSpawnedObject()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));

        if(InstantiatedPlace != null ) 
        {
            InstantiatedPlace.transform.eulerAngles += new Vector3(0, RotationInput * rotationMultiplier * Time.deltaTime, 0);
            if (Physics.Raycast(ray, out hit, 30, layerMask) && InstantiatedPlace != null && hit.collider.name.Equals("Terrain"))
            {
                if(GameManager.instance.state == GameManager.State.areaToDig)
                {
                    DiggingController checkIfCompleted = InstantiatedPlace.GetComponent<DiggingController>();
                    if(!hasPlacingItemFinished)
                    {
                        InstantiatedPlace.transform.position = new Vector3(hit.point.x, hit.point.y + diggingHeight, hit.point.z);
                    }
                }
                else if(GameManager.State.placeObject == GameManager.instance.state)
                {
                    if(!hasPlacingItemFinished)
                        InstantiatedPlace.transform.position = hit.point;
                    else 
                    {
                        //InstantiatedPlace = Instantiate(builtObjectPrefab, new Vector3(hit.point.x, hit.point.y, hit.point.z) , Quaternion.identity);
                        builtObject.SetActive(true);
                        InstantiatedPlace = builtObject;
                        InstantiatedPlace.transform.position = hit.point;
                        GameManager.instance.state = GameManager.State.placeObject;
                        hasPlacingItemFinished = false;
                    }
                }
            }
        }
    }

    public virtual void MouseLeftClick() 
    {
        if(baseBuildingFinished) return;

        RaycastHit hit;
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));

        if (Physics.Raycast(ray, out hit, 30) && InstantiatedPlace != null)
        {
            var build = InstantiatedPlace.GetComponentInChildren<PlacingItemController>();
            
            if(build != null)
            {
                if(GameManager.instance.state == GameManager.State.placeObject && hit.collider.tag.Equals("BuildingObject") && build.isReady)
                {
                    diggingArea.SetActive(false);
                    hasPlacingItemFinished = true;
                    GameManager.instance.state = GameManager.State.buildingObject;
                } 

                var interf = hit.collider.GetComponentInParent<BrickController>();
                if(interf != null) 
                {
                    interf.UdpateBrickObject();
                }
            }
        }

        if (Physics.Raycast(ray, out hit, 30, layerMask))
        {
            if(GameManager.instance.state == GameManager.State.digging)
            {
                DiggingController checkIfCompleted = InstantiatedPlace.GetComponent<DiggingController>();
                checkIfCompleted.diggingFinished.AddListener(OnDiggingCompleted);
                checkIfCompleted.UpdateDigging();
            }  

            if(hit.collider.name.Equals("Terrain") && GameManager.instance.state == GameManager.State.areaToDig)
            {
                GameManager.instance.state = GameManager.State.digging;
                hasPlacingItemFinished = true;
            }
        }
    }

    public virtual void MouseRightClick()
    {

    }

    public virtual void OnDiggingCompleted () {
        hasPlacingItemFinished = true;
    }

    public virtual void StartPlacingItem()
    {
        if(baseBuildingFinished) return;

        RaycastHit hit;
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));

        if (Physics.Raycast(ray, out hit, 15, layerMask))
        { 
            if(hit.collider.name.Equals("Terrain") && hasPlacingItemFinished)
            {
                InstantiatedPlace = diggingArea;
                InstantiatedPlace.transform.position = hit.point;
                //InstantiatedPlace = Instantiate(diggingArea, hit.point, Quaternion.identity);
                GameManager.instance.state = GameManager.State.areaToDig;
                hasPlacingItemFinished = false;
            }
        }
    
    }
}
