using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBuilding : MonoBehaviour
{
    public GameObject machineGunPrefab;
    public GameObject mortarPrefab;
    public GameObject barrierPrefab;
    public GameObject tentPrefab;
    public GameObject fencePrefab;
    private BuildingController buildingController;
    private GameObject instantiatedObject;
    private PlayerControl playerControl;

    void Awake()
    {
        playerControl = new PlayerControl();
        playerControl.Player.Rotation.performed += ctx => OnRotation(ctx);
        playerControl.Player.MouseClick.started += ctx => OnMouseLeftClick();
        playerControl.Player.MouseRightClick.started += ctx => OnMouseRightClick();
    }



    void Start()
    {
        
    }

    void Update()
    {
        if(Keyboard.current.digit2Key.wasPressedThisFrame && GameManager.instance.state == GameManager.State.nothing) {
            instantiatedObject = Instantiate(fencePrefab, transform.position, Quaternion.identity);
            buildingController = instantiatedObject.GetComponent<BuildingController>();
            buildingController.StartPlacingItem();
        }
        if(Keyboard.current.digit3Key.wasPressedThisFrame && GameManager.instance.state == GameManager.State.nothing) {
            instantiatedObject = Instantiate(machineGunPrefab, transform.position, Quaternion.identity);
            buildingController = instantiatedObject.GetComponent<BuildingController>();
            buildingController.StartPlacingItem();
        }
        if(Keyboard.current.digit4Key.wasPressedThisFrame && GameManager.instance.state == GameManager.State.nothing) {
            instantiatedObject = Instantiate(mortarPrefab, transform.position, Quaternion.identity);
            buildingController = instantiatedObject.GetComponent<BuildingController>();
            buildingController.StartPlacingItem();
        }
        if(Keyboard.current.digit5Key.wasPressedThisFrame && GameManager.instance.state == GameManager.State.nothing) {
            instantiatedObject = Instantiate(barrierPrefab, transform.position, Quaternion.identity);
            buildingController = instantiatedObject.GetComponent<BuildingController>();
            buildingController.StartPlacingItem();
        }
        if(Keyboard.current.digit6Key.wasPressedThisFrame && GameManager.instance.state == GameManager.State.nothing) {
            instantiatedObject = Instantiate(tentPrefab, transform.position, Quaternion.identity);
            buildingController = instantiatedObject.GetComponent<BuildingController>();
            buildingController.StartPlacingItem();
        }
        
    }

    private void OnMouseLeftClick()
    {
        if(buildingController != null)
            buildingController.MouseLeftClick();
    }

    private void OnMouseRightClick()
    {
        if(buildingController != null)
            buildingController.MouseRightClick();
    }
    private void OnEnable()
    {
        playerControl.Enable();
    }

    private void OnDisable()
    {
        playerControl.Disable();
    }

    private void OnRotation(InputAction.CallbackContext ctx) {
        if(buildingController != null){
            buildingController.RotationInput = ctx.ReadValue<float>();
        }
    }
}
 