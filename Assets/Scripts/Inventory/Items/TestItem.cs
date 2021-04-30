using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestItem : InventoryItemBase
{
    public override string Name{
        get{
            return "TestItem";
        }
    }
    public override string Type{
        get{
            return "Tool";
        }
    }
}
