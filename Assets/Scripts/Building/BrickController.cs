using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


public class BrickController : MonoBehaviour
{
    [System.Serializable]
    public class BuildingObjects
    {
        public GameObject gameO;
        public List<GameObject> DepengingOn;
        public Material EndingMaterial;
        public bool showAfterDependingFinished = false;
        [System.NonSerialized] public bool dependingFinished = false;
    }

    public List<BuildingObjects> ObjectsArray;
    public Material ToBuildMaterial;
    public Material AvailableMaterial;
    public Material UnavaiableMaterial;
    [System.NonSerialized]
    public bool bagsPlacingCompleted = false;
    public PlacingItemController properPlace;
    public BuildingController buildingController;
    public bool withoutPlacingController = false;

    void Update()
    {
        if (GameManager.instance.state == GameManager.State.placeObject && !bagsPlacingCompleted && properPlace != null)
        {
            foreach (var obj in ObjectsArray)
            {
                if (properPlace.isReady)
                {
                    ManageComponents(obj.gameO, ToBuildMaterial, false, null, null);
                    if (obj.DepengingOn.Count == 0)
                        ManageComponents(obj.gameO, AvailableMaterial, true, true, null);
                }
                else
                    ManageComponents(obj.gameO, UnavaiableMaterial, false, false, null);
            }
        }

        if(GameManager.instance.state == GameManager.State.placeObject && withoutPlacingController)
        {
            foreach (var obj in ObjectsArray)
            {
                ManageComponents(obj.gameO, ToBuildMaterial, false, null, null);
                if (obj.DepengingOn.Count == 0)
                    ManageComponents(obj.gameO, AvailableMaterial, true, true, null);
            }
        }
    }

    public void UdpateBrickObject()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        bagsPlacingCompleted = true;

        if (Physics.Raycast(ray, out hit, 7))
        {
            var obj = hit.collider.GetComponent<IBuildable>();

            if (obj == null) return;
            if (obj.available) // wejscie w dostepna cegle
            {
                bool allFinished = true;
                foreach (var gameObj in ObjectsArray)
                {
                    if (GameObject.ReferenceEquals(hit.collider.gameObject, gameObj.gameO) && DependingOnFinished(gameObj.DepengingOn))
                    {
                        ManageComponents(gameObj.gameO, gameObj.EndingMaterial, true, null, true);
                        Collider boxCollider = gameObj.gameO.GetComponent<Collider>();

                        if (boxCollider != null)
                        {
                            boxCollider.isTrigger = false;
                        }
                    }

                    var s = gameObj.gameO.GetComponent<IBuildable>();
                    if (!s.finished && DependingOnFinished(gameObj.DepengingOn)) 
                    {
                        ManageComponents(gameObj.gameO, AvailableMaterial, true, true, null);
                        gameObj.dependingFinished = true; 
                    }

                    if(gameObj.showAfterDependingFinished)
                    {
                        if(gameObj.dependingFinished) gameObj.gameO.SetActive(true);
                        else gameObj.gameO.SetActive(false);
                    }


                    if(!gameObj.gameO.GetComponent<IBuildable>().finished) allFinished = false;
                }
                if(allFinished) buildingController.baseBuildingFinished = true;
            }
        }
    }

    public void AddToDependingList(BuildingObjects buildingObject)
    {
        ObjectsArray.Add(buildingObject);
    }

    private bool DependingOnFinished(List<GameObject> depengingOn)
    {
        bool canAvailable = true;
        foreach (var d in depengingOn)
        {
            var scr = d.GetComponent<IBuildable>();
            if (!scr.finished) canAvailable = false;
        }
        return canAvailable;
    }

    private void ManageComponents(GameObject gameOb, Material material, bool? collider, bool? available, bool? finished)
    {
        if (material != null && gameOb.GetComponent<Renderer>() != null) gameOb.GetComponent<Renderer>().material = material;
        if (collider != null && gameOb.GetComponent<Collider>() != null) gameOb.GetComponent<Collider>().enabled = collider.Value;
        if (available != null && gameOb.GetComponent<IBuildable>() != null) gameOb.GetComponent<IBuildable>().available = available.Value;
        if (finished != null && gameOb.GetComponent<IBuildable>() != null) gameOb.GetComponent<IBuildable>().finished = finished.Value;
    }

}
