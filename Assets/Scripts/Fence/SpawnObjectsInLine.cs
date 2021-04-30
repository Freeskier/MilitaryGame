using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SpawnObjectsInLine : MonoBehaviour
{
    public GameObject prefab;
    public Transform startTransform, endTransform;
    public float prefabWidth = 2f;
    public float pickDistance = 5f;
    private Dictionary<int, GameObject> instantiatedObjects;
    private Transform pickedTransform;
    private bool isCarrying = false;


    void Start()
    {
        instantiatedObjects = new Dictionary<int, GameObject>();
    }

    void Update()
    {
        if(isCarrying)
        {
            SpawnSegmentsOfSpring();
            MovePickedObject();
        }
        else
        {
            PickUpPoint();
        }

        if(Mouse.current.leftButton.wasReleasedThisFrame) isCarrying = false;
    }

    void MovePickedObject() 
    {
        pickedTransform.position = Camera.main.transform.position + Camera.main.transform.forward * pickDistance;
    }

    void SpawnSegmentsOfSpring() 
    {
        float distance = Vector3.Distance(startTransform.position, pickedTransform.position);
        int numberOfObjects = Mathf.RoundToInt(distance / prefabWidth) ;
        startTransform.LookAt(pickedTransform);
        if (instantiatedObjects.Count > numberOfObjects) 
        {
            Destroy(instantiatedObjects[instantiatedObjects.Count - 1]);
            instantiatedObjects.Remove(instantiatedObjects.Count - 1);
        }
        else if(instantiatedObjects.Count < numberOfObjects)
        {
            var instantiated = Instantiate(prefab, pickedTransform.position, startTransform.rotation);
            instantiated.transform.SetParent(startTransform.parent);
            instantiatedObjects.Add(instantiatedObjects.Count, instantiated);
        }
        Vector3 dir = (pickedTransform.position - startTransform.position).normalized;

        foreach(var obj in instantiatedObjects)
        {
            obj.Value.transform.LookAt(startTransform);
            obj.Value.transform.position = (dir * (obj.Key * prefabWidth)) + startTransform.position;
        }
    }

    void PickUpPoint() 
    {
        if(Mouse.current.leftButton.wasPressedThisFrame)
        {
            Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0.5f));
            RaycastHit hit;

            if(Physics.Raycast(ray, out hit, 10f))
            {
                if(hit.collider.name.Equals("EndPoint")) 
                {
                    isCarrying = true;
                    pickedTransform = hit.transform;
                }

            }
        }
    }

    public void StartNew()
    {
        instantiatedObjects = new Dictionary<int, GameObject>();
    }
}
