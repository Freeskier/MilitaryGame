using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RadiostationController : MonoBehaviour
{
    public Transform hatch;
    public UIFollowObjectHint uiHint;
    public float hatchOpeningSpeed = 10f;
    bool hatchOpened = false;
    bool isOpening = false;

    void Start()
    {
        
    }

    void Update()
    {
        OpenCloseHatch();
    }

    private void OpenCloseHatch()
    {
        if(Mouse.current.leftButton.wasPressedThisFrame)
        {
            Vector2 mousePosition = Mouse.current.position.ReadValue();
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(mousePosition);

            if(Physics.Raycast(ray, out hit, 5f))
            {
                if(hit.collider.name.Equals("Hatch"))
                {
                    if(!isOpening)
                    {
                        uiHint.ShowHideHint();
                        StartCoroutine(MoveHatch());
                    } 
                }
            }
        }
    }

    IEnumerator MoveHatch()
    {
        float displacement = Time.deltaTime * hatchOpeningSpeed;
        displacement = hatchOpened? - displacement : displacement;
        float breakValue = hatchOpened? 197f : 357f;
        while(hatchOpened? (hatch.localEulerAngles.z > breakValue) : (hatch.localEulerAngles.z < breakValue))
        {
            isOpening = true;
            hatch.transform.Rotate(0, 0, displacement);
            yield return null;
        }
        isOpening = false;
        hatchOpened = !hatchOpened;
 
    }
}
