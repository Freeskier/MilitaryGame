using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacingItemController : MonoBehaviour
{
    public Transform[] transforms;
    public Vector3 size;
    [System.NonSerialized]
    public bool isReady = false;
    void Start()
    {
        
    }

    void Update()
    {
        bool canBuild2 = true;
        foreach(var t in transforms)
        {
            bool canBuild = false;
            bool terrain = false;
            Collider[] colliders = Physics.OverlapBox(t.position, size/2);
            foreach(var c in colliders)
            {
                if(c.tag.Equals("BuildingObject")) canBuild = true;
                if(c.name.Equals("Terrain")) terrain = true;
            }
            if(canBuild && canBuild2 && terrain) canBuild2 = true;
            else canBuild2 = false;
        }
        isReady = canBuild2;
    }

    void OnDrawGizmos()
    {
        foreach(var t in transforms)
        {
            Gizmos.DrawWireCube(t.position, size);
        }
    }
}
