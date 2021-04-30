using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBuildable
{
    bool available { get; set; }
    bool finished { get; set; }
}
