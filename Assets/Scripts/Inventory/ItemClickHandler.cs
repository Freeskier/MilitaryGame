using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemClickHandler : MonoBehaviour
{
    public Inventory Inventory;
    public ItemDragHandler dragHandler;
    public void OnItemClicked()
    {
        dragHandler = gameObject.transform.Find("ItemImage").GetComponent<ItemDragHandler>();

        IInventoryItem item = dragHandler.item;

        try
        {
            Inventory.UseItem(item);
        }
        catch (NullReferenceException e)
        {
            Debug.Log(e);
            Debug.Log("Empty slot");
        }
    }
}
