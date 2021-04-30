using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using  UnityEngine.InputSystem;

public class ItemDragHandler : MonoBehaviour, IDragHandler, IEndDragHandler
{  
    private Vector2 mouseProperLocation;

    private PlayerControl playerControl;
    public IInventoryItem item{get;set;}
    public void OnDrag(PointerEventData eventData){
        transform.position = Mouse.current.position.ReadValue();;
    }
    public void OnEndDrag(PointerEventData eventData){
        Inventory.instance.tryRemove(item);
        transform.localPosition = Vector3.zero;
    }
    private void Start() {
        playerControl = new PlayerControl();
    }
    private void Update() {
        //Vector2 mouseLocationOnScreen = playerControl.Player.MousePosition.ReadValue<Vector2>();
        //mouseProperLocation = Camera.main.ViewportToWorldPoint(mouseLocationOnScreen);
        //mouseProperLocation = Mouse.current.position.ReadValue();
    }
}
