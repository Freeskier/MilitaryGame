using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FenceController : BuildingController
{
    public GameObject nextSegmentPrefab;
    public GameObject lastPolePrefab;
    List<GameObject> segments;
    List<Transform> springStartPoints;
    public BrickController brickController;
    public SpawnObjectsInLine spawnObjectsInLine;

    public LayerMask terrainLayerMask;


    void Start()
    {
        segments = new List<GameObject>();
    }

    public override void Update()
    {
        MoveSpawnedObject();
        if(baseBuildingFinished)
        {
            if(springStartPoints == null)
            {
                springStartPoints = new List<Transform>();
                springStartPoints.Add(transform.Find("FenceObj").Find("Spring").Find("StartPoint"));
                foreach(var g in segments)
                {
                    Transform trans = g.transform.Find("FenceObj").Find("Spring").Find("StartPoint");
                    springStartPoints.Add(trans);
                }
            }
            if(springStartPoints.Count > 0)
                spawnObjectsInLine.startTransform = springStartPoints[0];

            if(springStartPoints.Count <= 1)
            {
                //KONIEC 
                spawnObjectsInLine.StartNew();
                spawnObjectsInLine.endTransform.gameObject.SetActive(false);
                spawnObjectsInLine.enabled = false;
                this.enabled = false;
                return;
            }

            if(Vector3.Distance(springStartPoints[1].position, spawnObjectsInLine.endTransform.position) < .1f)
            {
                springStartPoints.RemoveAt(0);
                spawnObjectsInLine.StartNew();
            }
            
        }
    }

    public override void StartPlacingItem()
    {
        InstantiatedPlace = builtObject;
        hasPlacingItemFinished = false;
        GameManager.instance.state = GameManager.State.placeObject;
    }

    public override void MoveSpawnedObject()
    {
        if (segments.Count > 0 && GameManager.instance.state == GameManager.State.placeObject && !hasPlacingItemFinished)
        {
            InstantiatedPlace = segments[segments.Count - 1];
            InstantiatedPlace.transform.eulerAngles += new Vector3(0, RotationInput * rotationMultiplier * Time.deltaTime, 0);
        }
        else
            base.MoveSpawnedObject();
    }


    public override void MouseLeftClick()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));


        if (!hasPlacingItemFinished && GameManager.instance.state == GameManager.State.placeObject)
        {
            Transform spawnTransform;
            Transform checkHeightTransform;
            GameObject lastSegment;
            if (segments.Count > 0)
            {
                lastSegment = segments[segments.Count - 1];
                spawnTransform = lastSegment.transform.Find("FenceObj").Find("NextSpawnPoint");
                checkHeightTransform = lastSegment.transform.Find("FenceObj").Find("SpawnHeight");
            }
            else
            {
                lastSegment = gameObject;
                spawnTransform = transform.Find("FenceObj").Find("NextSpawnPoint");
                checkHeightTransform = transform.Find("FenceObj").Find("SpawnHeight");
            }

            if (Physics.Raycast(checkHeightTransform.position, Vector3.down, out hit, terrainLayerMask))
            {
                Vector3 spawningPosition = new Vector3(spawnTransform.position.x, hit.point.y, spawnTransform.position.z);

                var nextSegmentInstantiated = Instantiate(nextSegmentPrefab, spawningPosition, lastSegment.transform.rotation);
                segments.Add(nextSegmentInstantiated);

                brickController.AddToDependingList(new BrickController.BuildingObjects()
                    {
                        gameO = nextSegmentInstantiated.transform.Find("FenceObj").Find("Long").gameObject,
                        DepengingOn = new List<GameObject>() { brickController.ObjectsArray[brickController.ObjectsArray.Count - 1].gameO},
                        EndingMaterial = brickController.ObjectsArray[brickController.ObjectsArray.Count - 1].EndingMaterial
                    });
                
                brickController.AddToDependingList(new BrickController.BuildingObjects()
                    {
                        gameO = nextSegmentInstantiated.transform.Find("FenceObj").Find("Net").gameObject,
                        DepengingOn = new List<GameObject>() { brickController.ObjectsArray[brickController.ObjectsArray.Count - 1].gameO},
                        EndingMaterial = brickController.ObjectsArray[brickController.ObjectsArray.Count - 1].EndingMaterial
                    });
            }
        }

        if (Physics.Raycast(ray, out hit, 30))
        {
            if(GameManager.instance.state == GameManager.State.digging)
            {
                FenceDigging digController = hit.collider.GetComponent<FenceDigging>();
                if(digController != null) 
                {
                    digController.DiggingWithPoint(hit.collider.bounds.center);
                    OnSegmentDiggingCompleted();
                }
            }  

            if(GameManager.instance.state == GameManager.State.buildingObject)
            {
                if(brickController != null) 
                {
                    brickController.UdpateBrickObject();
                }
            }
        }
    }

    public override void MouseRightClick()
    {
        hasPlacingItemFinished = true;
        GameManager.instance.state = GameManager.State.digging;
        segments.Remove(InstantiatedPlace);
        Destroy(InstantiatedPlace);
        InstantiatedPlace = null;

        brickController.ObjectsArray.RemoveAt(brickController.ObjectsArray.Count - 1);
        brickController.ObjectsArray.RemoveAt(brickController.ObjectsArray.Count - 1);

        Transform spawnTransform;
        Transform checkHeightTransform;
        GameObject lastSegment;
        RaycastHit hit;
        

        if (segments.Count > 0)
            {
                lastSegment = segments[segments.Count - 1];
                spawnTransform = lastSegment.transform.Find("FenceObj").Find("NextSpawnPoint");
                checkHeightTransform = lastSegment.transform.Find("FenceObj").Find("SpawnHeight");
            }
            else
            {
                lastSegment = gameObject;
                spawnTransform = transform.Find("FenceObj").Find("NextSpawnPoint");
                checkHeightTransform = transform.Find("FenceObj").Find("SpawnHeight");
            }

        if (Physics.Raycast(checkHeightTransform.position, Vector3.down, out hit, terrainLayerMask))
            {
                Vector3 spawningPosition = new Vector3(spawnTransform.position.x, hit.point.y, spawnTransform.position.z);

                var nextSegmentInstantiated = Instantiate(lastPolePrefab, spawningPosition, lastSegment.transform.rotation);
                segments.Add(nextSegmentInstantiated);

                brickController.AddToDependingList(new BrickController.BuildingObjects()
                    {
                        gameO = nextSegmentInstantiated.transform.Find("FenceObj").Find("Short").gameObject,
                        DepengingOn = new List<GameObject>() { brickController.ObjectsArray[brickController.ObjectsArray.Count - 1].gameO},
                        EndingMaterial = brickController.ObjectsArray[brickController.ObjectsArray.Count - 1].EndingMaterial
                    });
            }

    }

    void OnSegmentDiggingCompleted()
    {
        GameManager.instance.state = GameManager.State.digging;
        bool hasDiggingFinished = true;
        foreach(var segment in segments)
        {
            var diggingScript = segment.GetComponentInChildren<FenceDigging>();
            if(!diggingScript.segmentFinished) hasDiggingFinished = false;
            if(!GetComponentInChildren<FenceDigging>().segmentFinished) hasDiggingFinished = false;
        }
        if( hasDiggingFinished) 
        {
            GameManager.instance.state = GameManager.State.buildingObject;
        }
    }

}
