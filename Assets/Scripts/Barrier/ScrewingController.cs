using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ScrewingController : MonoBehaviour
{
    public Transform screwDeviceTransform;
    public Transform screwDeviceGrabTransform;
    public Transform playerTransform;
    public RotateObjectWithMouse rotationScript;

    private PlayerControl playerControl;
    private GameObject screwDeviceReference;
    private bool screwingStarted = false;
    public float speed = 5f;

//screw driver
    public GameObject screwDriverPrefab;
    public BuildingController buildingController;
    public Transform camTransform;
    private GameObject screwDriverObject;


    void Awake()
    {
        playerControl = new PlayerControl();
        playerControl.Player.MouseClick.performed += ctx => MouseClick();
    }

    void Update()
    {
        if(screwingStarted && screwDeviceReference != null)
        {
            Screw screw = screwDeviceReference.GetComponentInParent<Screw>();
            if(Mouse.current.leftButton.isPressed && !screw.finished && rotationScript.ScrewObject(screwDeviceTransform, screwDeviceGrabTransform, screwDeviceReference.transform)) 
                screw.finished = true; 
        }
    }

    private void MouseClick()
    {
        Vector2 mousePos = Mouse.current.position.ReadValue();
        Vector3 mouse = new Vector3(mousePos.x ,mousePos.y, 0);
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(mouse);
        
        if (Physics.Raycast(ray, out hit))
        {
            if(hit.collider.tag.Equals("Screw"))
            {
                if(screwDriverObject == null && buildingController.baseBuildingFinished)
                {
                    screwDriverObject = Instantiate(screwDriverPrefab, camTransform.position, Quaternion.identity);
                }

                StartCoroutine(MoveTransform(screwDeviceTransform, hit.collider.transform.parent.transform.position));
                screwDeviceReference = hit.collider.transform.parent.gameObject;
            }

        }
    }

    IEnumerator MoveTransform(Transform transformFirst, Vector3 hitPosition) 
    {
        transformFirst.rotation = transform.rotation;
        while(Vector3.Distance(transformFirst.position, hitPosition) > 0.005f)
        {
            screwingStarted = false;
            transformFirst.position = Vector3.Lerp(transformFirst.position, hitPosition, Time.deltaTime * speed);
            yield return null;
        }
        screwingStarted = true;
        yield return null;
    }

    private void OnDisable()
    {
        playerControl.Disable();
    }

    private void OnEnable()
    {
        playerControl.Enable();
    }
}
