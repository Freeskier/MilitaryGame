using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnobRotation : MonoBehaviour
{
    [HideInInspector]
    public float totalRotationValue = 0f;
    public Vector2 minMaxRotationRange;

    public float RotationValue
    {
        get{
            return totalRotationValue / (minMaxRotationRange.y - minMaxRotationRange.x);
        }

    }

    public void RotateKnob(Vector3 knobScreenPos, Transform knobTransform, Vector3 mousePosition)
    {
        float angle = - Mathf.Atan2(knobScreenPos.x - mousePosition.x, knobScreenPos.y - mousePosition.y) * Mathf.Rad2Deg - 180;
        float pastXrotation = knobTransform.localRotation.eulerAngles.x;
        
        Quaternion newRotation = Quaternion.Euler(angle, 0, 0);
        float actualXrotation = newRotation.eulerAngles.x;
        float rotationDiff = (knobScreenPos.y - mousePosition.y) < 0? pastXrotation - actualXrotation : actualXrotation - pastXrotation;
            
        if(Mathf.Abs(rotationDiff) > .001f && Mathf.Abs(rotationDiff) < 10f)
        {
            totalRotationValue += rotationDiff;
        }

        knobTransform.localRotation = newRotation; 
    }
}


