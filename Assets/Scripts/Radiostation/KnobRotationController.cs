using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class KnobRotationController : MonoBehaviour
{
    public PlotDrawer plotDrawer;
    public PlotDrawer mirrorPlot;
    bool rotatingStarted = false;      
    Vector3 knobScreenPos; 
    Vector3 mousePosition;
    Transform knobTransform;
    KnobRotation knobRotationRef;


    void Update()
    {
        CheckForFinishSettingUp();
        
        if(Mouse.current.leftButton.wasReleasedThisFrame)
        {
            rotatingStarted = false;
            knobRotationRef = null;
            knobTransform = null;
            mousePosition = Vector3.zero;
        } 

        if(Mouse.current.leftButton.wasPressedThisFrame)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());

            mousePosition = Mouse.current.position.ReadValue();

            if(Physics.Raycast(ray, out hit, 2f))
            {
                if(hit.collider.tag.Equals("Knob"))
                {
                    knobScreenPos = Camera.main.WorldToScreenPoint(hit.collider.transform.position);
                    knobTransform = hit.collider.transform;
                    rotatingStarted = true;
                    knobRotationRef = hit.collider.GetComponent<KnobRotation>();
                }
            }
        }

        if(rotatingStarted)
        {
            mousePosition = Mouse.current.position.ReadValue();
            knobRotationRef.RotateKnob(knobScreenPos, knobTransform, mousePosition);
            float adjustValue = knobRotationRef.RotationValue;
            
            switch(knobTransform.name)
            {
                case "Knob1":
                    if(adjustValue > 0 && adjustValue < 1)
                        plotDrawer.noise = adjustValue * 4 - 3;
                break;

                case "Knob2":
                    if(adjustValue > 0 && adjustValue < 1)
                        plotDrawer.amplitude = adjustValue * 50f + 20;
                break;

                case "Knob3":
                    if(adjustValue > 0 && adjustValue < 1)
                        plotDrawer.posCount = (int) (adjustValue * 150f - 1) + 5;
                break;
            }

            
        }
    }

    private void CheckForFinishSettingUp()
    {
        if(Mathf.Abs(plotDrawer.amplitude - mirrorPlot.amplitude) < .5f &&
        Mathf.Abs(plotDrawer.noise - mirrorPlot.noise) < .01f)
        {
            Debug.Log("KONIEC!");
        }
    }
}
