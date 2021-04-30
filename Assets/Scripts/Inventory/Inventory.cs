using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    #region Singleton

    public static Inventory instance;

    void Awake()
    {
        instance = this;
    }

    #endregion
    private const int SLOTS = 6;
    private bool isUpdated;
    public Dictionary<String, List<IInventoryItem>> mDict = new Dictionary<string, List<IInventoryItem>>();
    public event EventHandler<InventoryEventArgs> itemAdded;
    public event EventHandler<InventoryEventArgs> itemRemoved;
    public event EventHandler<InventoryEventArgs> itemUsed;
    public void AddItem(IInventoryItem item)
    {   

        if (mDict.Count == SLOTS)
        {
            return;
        }
        if (!tryAdd(item))
        {
            addNewType(item);
        }

        
    }

    private bool tryAdd(IInventoryItem item)
    {
        if (mDict.ContainsKey(item.Name))
        {
            mDict[item.Name].Add(item);
            item.OnPickup();
            if (itemAdded != null)
            {
                itemAdded(this, new InventoryEventArgs(item));
                return true;
            }
        }

        //TODO: dodac exception

        return false;
    }

    private void addNewType(IInventoryItem item)
    {
        mDict.Add(item.Name, new List<IInventoryItem>());
        mDict[item.Name].Add(item);
        item.OnPickup();
        if (itemAdded != null)
        {
            itemAdded(this, new InventoryEventArgs(item));
        }
        //TODO: exception jakby sie nie udało
    }
    // public void RemoveItem(IInventoryItem item){
    //     if(!tryRemove(item)){
    //         removeItem(item);
    //     }
    // }
    public void tryRemove(IInventoryItem item)
    {
        int last_elemnt;

        last_elemnt = mDict[item.Name].Count - 1;
        IInventoryItem temp = mDict[item.Name][last_elemnt];
        item.OnDrop();

        if (itemRemoved != null)
        {
            itemRemoved(this, new InventoryEventArgs(temp));
        }

        mDict[item.Name].Remove(temp);
    }
    // private void removeItem (IInventoryItem item){
    //     mItems.Remove(item);
    //     item.OnDrop();
    //     if(itemRemoved!=null){
    //         itemRemoved(this, new InventoryEventArgs(item));
    //         Debug.Log(item.amount);
    //     }
    // }     
    public void UseItem(IInventoryItem item)
    {
        if (itemUsed != null)
        {
            itemUsed(this, new InventoryEventArgs(item));
        }
    }
}
