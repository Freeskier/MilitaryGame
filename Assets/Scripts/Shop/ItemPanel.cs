using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemPanel : MonoBehaviour
{
    [SerializeField]
    private Transform itemPanel;
    [SerializeField]
    private GameObject spawnPoint;

    private string GetItemName(){
        Transform textComp = itemPanel.Find("ItemText");
        Text itemText = textComp.GetComponent<Text>();

        return itemText.text;
    }

    public void OnClick(){
        Instantiate(Resources.Load(GetItemName()),spawnPoint.transform.position, spawnPoint.transform.rotation);
    }
}
