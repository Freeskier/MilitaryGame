using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopUI : MonoBehaviour
{
    [SerializeField]
    private Transform itemPanel;
    [SerializeField]
    private InventoryItemBase Testerm;
    [SerializeField]
    GameObject spawnItemPoint;
    private List<InventoryItemBase> itemList;
    private Transform shopPanel;
    private List<Transform> itemPanels;

    private void Awake() {
        shopPanel = transform.Find("ShopPanel");
        itemPanels = new List<Transform>();
        itemList = new List<InventoryItemBase>();

        itemList.Add(Testerm);
    }

    public void Start() {
        FillShop();
    }

    private void FillShop(){
        itemPanels.Add(FillSlot(Testerm));
        itemPanels.Add(FillSlot(Testerm));
        itemPanels.Add(FillSlot(Testerm));
    }

    private Transform FillSlot(InventoryItemBase item){
        Transform panel = Instantiate(itemPanel, shopPanel, false);
        SetPanelText(panel, item.Name);
        SetPanelImage(panel, item.image);

        return panel;
    }

    private void SetPanelText(Transform panel, string name){
        Transform tmp = panel.Find("ItemText");
        Text txt = tmp.GetComponent<Text>();
        txt.text = name;
    }
    private void SetPanelImage(Transform panel, Sprite image){
        Image img = panel.GetChild(0).GetComponent<Image>();
        img.sprite = image;
    }
}
