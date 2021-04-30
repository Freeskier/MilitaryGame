using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public State state;
    public enum State {nothing, areaToDig, digging, placeObject, buildingObject}
    public GameObject machineGunPrefab;



    void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else 
            Destroy(gameObject);
    }

    void Start()
    {
        state = State.nothing;
    }

    void Update()
    {
        switch(state) {

            case State.nothing:
                
                break;

            case State.areaToDig:
                break;

            case State.digging:
                    //Debug.Log("KOPANIE!!");

                break;

            case State.buildingObject:

                break;
        }

    }
}

