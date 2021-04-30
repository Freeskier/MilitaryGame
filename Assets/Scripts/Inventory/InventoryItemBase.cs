using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InventoryItemBase : MonoBehaviour, IInventoryItem
{
    public virtual string Name
    {
        get
        {
            return "base_item";
        }
    }
    public virtual string Type
    {
        get
        {
            return "base_type";
        }
    }
    public int _amount = 0;
    public int amount
    {
        get { return _amount; }
        set { _amount = value; }
    }
    public Sprite _Image = null;

    public Sprite image
    {
        get
        {
            return _Image;
        }
    }

    public GameObject obj
    {
        get
        {
            return gameObject;
        }
    }
    public virtual void OnPickup()
    {
        gameObject.SetActive(false);
    }
    public void increse(int temp)
    {
        _amount = _amount + temp;
    }
    public void OnDrop()
    {
        PlayerControl playerControl = new PlayerControl();
        RaycastHit hit = new RaycastHit();
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());

        Debug.Log("check");
        if (Physics.Raycast(ray, out hit, 100))
        {
            gameObject.SetActive(true);
            gameObject.transform.SetParent(null);
            gameObject.transform.position = hit.point + new Vector3(0,1,0);
            gameObject.GetComponent<Collider>().enabled = true;
            gameObject.GetComponent<Rigidbody>().useGravity = true;
        }
    }
    public virtual void OnUse()
    {
        
    }
}
