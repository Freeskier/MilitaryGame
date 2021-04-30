using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Concrete : InventoryItemBase
{
    Camera cam;
    public override string Name{
        get{
            return "Concrete";
        }
    }
    public override string Type{
        get{
            return "Material";
        }
    }
    public override void OnUse()
    {
        base.OnUse();
    }
    public void tryPut(){
        Ray ray = cam.ScreenPointToRay(Mouse.current.position.ReadValue());
        RaycastHit hit = new RaycastHit();
        if (Physics.Raycast(ray, out hit, 5)){
            MeshCollider box = hit.collider as MeshCollider;
            if (box == null) Debug.LogWarning("Collider is not a MeshCollider!");
            put_in_barrow();
        }
    }
    public void put_in_barrow(){
        
    }
}
