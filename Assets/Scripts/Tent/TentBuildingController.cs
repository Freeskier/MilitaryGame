using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TentBuildingController : BuildingController
{
    bool tentBuildingFinished = false;
    void Start()
    {
        
    }

    public override void Update()
    {
        MoveSpawnedObject();

        if(baseBuildingFinished)
        {

        }

        if(tentBuildingFinished)
        {
            GameManager.instance.state = GameManager.State.nothing;
            builtObject.GetComponent<BrickController>().enabled = false;
            this.enabled = false;
        }
    }
}
