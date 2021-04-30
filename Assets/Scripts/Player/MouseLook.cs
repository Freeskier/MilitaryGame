using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MouseLook : MonoBehaviour
{
    [SerializeField] float sensivityX = 6f;
    float mouseX;
    Camera cam;

    private void Awake() {
        cam = Camera.main;
    }

    private void Update() {
        if(!Keyboard.current.ctrlKey.isPressed){
            //transform.Rotate(Vector3.up, mouseX * Time.deltaTime);
            Quaternion rot = cam.transform.rotation;
            transform.rotation = Quaternion.Euler(0f, rot.eulerAngles.y, 0f);
        }
    }
    public void ReciveInput(Vector2 mouseInput){
        mouseX = mouseInput.x * sensivityX;
    }
}
