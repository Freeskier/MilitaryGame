using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraSwitcher : MonoBehaviour
{

    [SerializeField]
    private InputAction action;
    private Animator animator;
    private bool isPlayerCam = true;
    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        action.performed += ctx => SwitchCamera();
    }

    private void SwitchCamera()
    {
        if(isPlayerCam) 
        {
            animator.Play("PlayerCamera");
            Cursor.lockState = CursorLockMode.Locked;
        }
        else if(!isPlayerCam && Keyboard.current.spaceKey.isPressed) 
        {
            animator.Play("BarrierCamera");
            Cursor.lockState = CursorLockMode.None;
        }
        else if(!isPlayerCam && Keyboard.current.oKey.isPressed){
            animator.Play("ShopCamera");
            Cursor.lockState = CursorLockMode.None;
        }
        else if(!isPlayerCam && Keyboard.current.pKey.isPressed){
            animator.Play("RadioCamera");
            Cursor.lockState = CursorLockMode.None;
        }
        isPlayerCam = !isPlayerCam;
    }

    private void OnEnable()
    {
        action.Enable();
    }

    private void OnDisable()
    {
        action.Disable();
    }
}
