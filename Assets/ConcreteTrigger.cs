using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ConcreteTrigger : MonoBehaviour
{
    Camera cam;
    private void Awake() {
        cam = Camera.main;
    }
    private void Update() {
        if(Keyboard.current.fKey.wasPressedThisFrame){
            OnGetItemFromBarrow();
        }
    }
    private void OnTriggerEnter(Collider other) {
        int tmp = transform.parent.gameObject.GetInstanceID();
        
        if(other.tag == "Concrete"){
            EventManager.instance.OnConcretePutInBarrow(tmp, other);
            Debug.Log("trigger");
        }
    }

    private void OnGetItemFromBarrow(){
        Ray ray = cam.ScreenPointToRay(Mouse.current.position.ReadValue());
        RaycastHit hit = new RaycastHit();
        if (Physics.Raycast(ray, out hit, 5)){
            MeshCollider box = hit.collider as MeshCollider;
            if (box == null){
                Debug.LogWarning("Collider is not a BoxCollider!");
                return;
            }
            if(box.tag == "Barrow"){
                EventManager.instance.OnGetFromBarrow();
            }
        }
    }
}
