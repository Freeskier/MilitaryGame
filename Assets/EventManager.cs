using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static EventManager instance;

    private void Awake() {
        if(instance == null) instance = this;
        else Destroy(this);
    }

    public event Action<int, Collider> ConcentratePutInBarrow;
    public event Action GetFromBarrow;

    public void OnConcretePutInBarrow(int instanceId, Collider collider){
        ConcentratePutInBarrow?.Invoke(instanceId, collider);
        Debug.Log("manager");
    }
    public void OnGetFromBarrow(){
        GetFromBarrow?.Invoke();
    }
}
