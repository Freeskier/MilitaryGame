using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour, IBuildable
{
    public bool available { get; set; }
    public bool finished { get; set; }
}
