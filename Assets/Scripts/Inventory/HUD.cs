using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading;

public class HUD : MonoBehaviour
{
    public Mutex mutexAddRemove;
    public Dictionary<string, Transform> hDict = new Dictionary<string, Transform>();
    void Start()
    {
        Inventory.instance.itemAdded += InventoryScript_ItemAdded;
        Inventory.instance.itemRemoved += InventoryScript_ItemRemoved;
        mutexAddRemove = Mutex.OpenExisting("InventoryAcessMutex");
    }
    private void AddNewSlot(Transform inventoryPanel, InventoryEventArgs e)
    {
        Transform tmp_slot = getFreeSlot(inventoryPanel);
        Image image = tmp_slot.GetChild(0).GetChild(0).GetComponent<Image>();
        Text text = tmp_slot.GetComponentInChildren<Text>();
        ItemDragHandler itemDragHandler = tmp_slot.GetChild(0).GetChild(0).GetComponent<ItemDragHandler>();

        hDict.Add(e.Item.Name, tmp_slot);
        text.enabled = image.enabled = true;
        image.sprite = e.Item.image;
        text.text = "1";
        itemDragHandler.item = e.Item;
    }
    private void UpdateSlot(Transform inventoryPanel, InventoryEventArgs e)
    {
        uint newValue;

        newValue = uint.Parse(hDict[e.Item.Name].GetComponentInChildren<Text>().text) + 1;
        hDict[e.Item.Name].GetComponentInChildren<Text>().text = newValue.ToString();
    }

    private Transform getFreeSlot(Transform inventoryPanel)
    {
        foreach (Transform tmp in inventoryPanel)
        {
            if (!tmp.GetChild(0).GetChild(0).GetComponent<Image>().enabled)
            {
                return tmp;
            }
        }
        //TODO: exception
        return null;
    }
    private void InventoryScript_ItemAdded(object sender, InventoryEventArgs e)
    {
        Transform inventoryPanel = transform.Find("Inventory");

        if (hDict.ContainsKey(e.Item.Name))
        {
            UpdateSlot(inventoryPanel, e);
        }
        else
        {
            AddNewSlot(inventoryPanel, e);
        }
    }
    private void InventoryScript_ItemRemoved(object sender, InventoryEventArgs e)
    {
        Transform inventoryPanel = transform.Find("Inventory");
        Transform tmp_slot = hDict[e.Item.Name];
        ItemDragHandler itemDragHandler = tmp_slot.GetChild(0).GetChild(0).GetComponent<ItemDragHandler>();
        int newValue;

        newValue = Inventory.instance.mDict[e.Item.Name].Count;
        itemDragHandler.item = Inventory.instance.mDict[e.Item.Name][newValue - 1];

        if (newValue == 1)
        {
            Image image = tmp_slot.GetChild(0).GetChild(0).GetComponent<Image>();
            Text text = tmp_slot.GetComponentInChildren<Text>();
            mutexAddRemove.WaitOne();
            hDict.Remove(e.Item.Name);
            mutexAddRemove.ReleaseMutex();
            text.enabled = image.enabled = false;
        }
        else
        {
            newValue--;
            mutexAddRemove.WaitOne();
            hDict[e.Item.Name].GetComponentInChildren<Text>().text = newValue.ToString();
            mutexAddRemove.ReleaseMutex();
        }
    }
}