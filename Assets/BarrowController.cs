using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;
using System.Threading;

public class BarrowController : MonoBehaviour
{
    private int _instanceId;
    List<GameObject> Container;
    int counter;
    Mutex mutex;
    GameObject barrow;
    GameObject player;
    GameObject spot;
    Camera cam;
    bool entered;

    private void Start(){
        cam = Camera.main;
        barrow = GameObject.Find("Barrow");
        player = GameObject.Find("Player");
        spot = GameObject.Find("BarrowSpot");
        mutex = new Mutex(true, "BarrowMutex");
        _instanceId = transform.gameObject.GetInstanceID();
        Container = new List<GameObject>();
        entered = false;

        EventManager.instance.ConcentratePutInBarrow += put_in;
        EventManager.instance.GetFromBarrow += get_from_barrow;
    }
    private void Update() {
        if(Keyboard.current.eKey.wasPressedThisFrame && !entered){
            try_enter();
            Debug.Log(entered);
        }
        else if (Keyboard.current.eKey.wasPressedThisFrame && entered){
            leave_barrow();
            Debug.Log(entered);
        }
    }

    private void try_enter(){
        Ray ray = cam.ScreenPointToRay(Mouse.current.position.ReadValue());
        RaycastHit hit = new RaycastHit();
        if (Physics.Raycast(ray, out hit, 5)){
            BoxCollider box = hit.collider as BoxCollider;
            Debug.Log(box);
            if (box == null) Debug.LogWarning("Collider is not a BoxCollider!");
            else enter_barrow(box);
        }
    }
    private void enter_barrow(BoxCollider box){
        if(box.tag == "Barrow"){ //TODO poprawić 
            barrow.transform.SetParent(spot.transform);
            Quaternion spotRotation = spot.transform.rotation;
            barrow.transform.position = spot.transform.position;
            barrow.transform.rotation = Quaternion.Euler(7f,spotRotation.eulerAngles.y,spotRotation.eulerAngles.z);
            Debug.Log("entered");
            entered = true;
        }
    }

    private void leave_barrow(){
        barrow.transform.localPosition += Vector3.forward;
        barrow.transform.SetParent(null);
        Debug.Log("left");
        entered = false;
    }

    private void put_in(int id, Collider other){
        if(id == _instanceId){
            GameObject concrete = other.gameObject;
            Container.Add(concrete);
            counter++;
            concrete.gameObject.SetActive(false);

            Debug.Log(Container.Count);
            Debug.Log(counter);
        }
    }
    private void get_from_barrow(){
        if(Container.Count != 0){
            Debug.Log("Before" + Container.Count + counter);

            GameObject tmp = Container[Container.Count - 1];
            tmp.gameObject.SetActive(true);
            tmp.transform.position = barrow.transform.position + new Vector3(0f,0f,3f);

            Container.Remove(tmp);
            counter--;
            Debug.Log("After" + Container.Count + counter);
        }else{
            Debug.Log("Barrow is empty");
            return;
        }
    }
}
