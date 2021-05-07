using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class KnobRotation : MonoBehaviour
{
    [HideInInspector]
    public float totalRotationValue = 0f;
    public Vector2 minMaxRotationRange;
    float lastAngle;
    Vector3 initialPosition = Vector3.zero;

    public float RotationValue
    {
        get{
            return totalRotationValue / (minMaxRotationRange.y - minMaxRotationRange.x);
        }
    }

    public void RotateKnob(Vector3 knobScreenPos, Transform knobTransform, Vector3 mousePosition)
    {
        Vector2 dir = mousePosition - knobScreenPos;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg + 90f;
        float angleDiff = angle - lastAngle;

        if(Mathf.Abs(angleDiff) > 30f)
            lastAngle = angle;
        
        if(totalRotationValue + angleDiff > minMaxRotationRange.y || totalRotationValue + angleDiff < minMaxRotationRange.x || Mathf.Abs(angleDiff) > 30f)
            return;

        totalRotationValue += angleDiff;
        knobTransform.localRotation = Quaternion.Euler(totalRotationValue, 0, 0); 
        lastAngle = angle;
    }

    public void ScrewWithRotation(Vector3 knobScreenPos, Transform knobTransform, Vector3 mousePosition, float screwingDepth, Vector3 axis)
    {
        RotateKnob(knobScreenPos, knobTransform, mousePosition);

        if(initialPosition == Vector3.zero) initialPosition = knobTransform.localPosition;
        var displacement = axis * RotationValue * screwingDepth + initialPosition;
        knobTransform.localPosition = displacement;
    }
    
}


