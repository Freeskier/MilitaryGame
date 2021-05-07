using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MoveObjectWithMouse : MonoBehaviour
{
    private bool isCarrying = false;
    private Transform pickedTransform;
    private float pickDistance;

    void Start()
    {
        
    }

    void Update()
    {
        if(Mouse.current.leftButton.wasReleasedThisFrame) isCarrying = false;

        if(isCarrying)
        {
            MovePickedObject();
        }
        else
        {
            PickUpPoint();
        }

    }

    void MovePickedObject() 
    {            
        Vector2 mousePosition = Mouse.current.position.ReadValue();


        pickedTransform.position = Camera.main.ScreenToWorldPoint (mousePosition) + Camera.main.transform.forward * pickDistance;
    }


    void PickUpPoint() 
    {
        if(Mouse.current.leftButton.wasPressedThisFrame)
        {
            Vector2 mousePosition = Mouse.current.position.ReadValue();
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(mousePosition);

            if(Physics.Raycast(ray, out hit, 10f))
            {
                if(hit.collider.name.Equals("EndPoint")) 
                {
                    isCarrying = true;
                    pickedTransform = hit.transform;
                    pickDistance = hit.distance;
                }

            }
        }
    }
}
