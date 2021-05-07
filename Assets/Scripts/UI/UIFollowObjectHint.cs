using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIFollowObjectHint : MonoBehaviour
{
    public Transform target;
    public TMP_Text hintText;
    [HideInInspector]
    public bool followingStarted = false;

    void Start()
    {
        
    }

    void Update()
    {
        if(followingStarted)
        {
            Vector2 screenPosition = Camera.main.WorldToScreenPoint(target.position);
            hintText.GetComponent<RectTransform>().position = screenPosition;
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        if(collider.name.Equals("Player"))
        {
            ShowHideHint();
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if(collider.name.Equals("Player"))
        {
            ShowHideHint();
        }

    }


    public void ShowHideHint()
    {
        if(followingStarted)
        {
            hintText.enabled = false;
            followingStarted = false;
        }
        else
        {
            hintText.enabled = true;
            followingStarted = true;
        }
        
    }
}
