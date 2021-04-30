using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Screw : MonoBehaviour, IBuildable
{
    public bool available { get; set; }
    public bool finished { get; set; }
}
