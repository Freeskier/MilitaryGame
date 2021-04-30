using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BoundsOfTerrain : MonoBehaviour
{
    public GameObject Text;
    public Transform startPoint;
    public Transform playerTransform;
    public TextMeshProUGUI secondsText;
    bool startCount = false;
    int counter;
    float timer;
    int timePassed = 0;

    void Update()
    {
        if(startCount)
        {
            timer += Time.deltaTime;
            timePassed = (int)timer;
            secondsText.SetText((5 - timePassed).ToString());

            if(5 - timePassed == 0) 
            {
                playerTransform.position = startPoint.transform.position;
            }
        }
        else 
        {
            timer = 0;  
            timePassed = 0;
            Text.SetActive(false);
        }
    }
    void OnTriggerExit(Collider other)
    {
        if(other.tag.Equals("Player"))
        {
            startCount = true;
            Text.SetActive(true);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag.Equals("Player"))
        {
            startCount = false;
        }
    }
}
