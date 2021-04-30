using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RotateObjectWithMouse : MonoBehaviour
{
    public float speed = 10f;
    public float HowMuchRotation;
    private PlayerControl playerControl;
    private const float ROTATION_DELTA = .15f;
    private const int MAX_DEGREES = 2000;
    float pastRotation = 0f;
    Vector2 pastMousePos;
    float sumRotation = 0f;
    float progressDiff = 0f;
    float progress;

    void Awake()
    {
        playerControl = new PlayerControl();
    }


    public bool ScrewObject(Transform trans, Transform grabTransform, Transform RotateWith)
    {
        Vector2 mousePos = Mouse.current.position.ReadValue();        
        Vector2 dir = Camera.main.WorldToScreenPoint(grabTransform.position);
        
        float angle = - Mathf.Atan2(dir.x - mousePos.x, dir.y - mousePos.y) * Mathf.Rad2Deg;
        Quaternion rotation = transform.rotation * Quaternion.AngleAxis(angle, Vector3.right);
        
        if(Mathf.Abs(mousePos.x - pastMousePos.x) > 1 || Mathf.Abs(mousePos.y - pastMousePos.y) > 1)
        {
            if(angle < pastRotation && Mathf.Abs(angle - pastRotation) < 170 && sumRotation <= MAX_DEGREES)
            {
                sumRotation += Mathf.Abs(pastRotation - angle);
                progress = sumRotation/MAX_DEGREES;
                
                trans.localPosition = Vector3.Lerp(trans.localPosition, trans.localPosition + new Vector3(ROTATION_DELTA, 0, 0), progress - progressDiff);
                RotateWith.localPosition = Vector3.Lerp(RotateWith.localPosition, RotateWith.localPosition + new Vector3(ROTATION_DELTA, 0, 0), progress - progressDiff);
                
                trans.rotation = Quaternion.Slerp(trans.rotation, rotation, speed * Time.deltaTime);
                RotateWith.rotation = Quaternion.Slerp(trans.rotation, rotation, speed * Time.deltaTime);

                progressDiff = progress;

                if(sumRotation >= MAX_DEGREES)
                {
                    progress = 0f;
                    progressDiff = 0f;
                    sumRotation = 0f;
                    return true;
                }
            }  
        }
        
        pastRotation = angle;
        pastMousePos = mousePos;

        return false;
    }

    void OnDisable()
    {
        playerControl.Disable();
    }

    void OnEnable()
    {
        playerControl.Enable();
    }
}
