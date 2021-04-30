using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInventoryItem{
    string Name{get;}
    int amount{get; set;}
    void increse(int temp);
    string Type{get;}
    Sprite image{get;}
    GameObject obj {get;}
    void OnPickup();
    void OnDrop();
    void OnUse();
}

public class InventoryEventArgs : EventArgs{
    public IInventoryItem Item;
    public InventoryEventArgs(IInventoryItem item){
        Item = item;
    }
}
