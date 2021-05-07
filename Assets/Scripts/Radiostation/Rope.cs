using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Rope : MonoBehaviour
{

    public Transform startTransform;
    public Transform endTransform;
    public float stiffness = 1f;
    public Color cableColor;

    private List<RopeSegment> ropeSegments = new List<RopeSegment>();
    private List<GameObject> linePoints = new List<GameObject>();
    private List<GameObject> lineConnectors = new List<GameObject>();
    private float ropeSegLen = 0.001f;
    private int segmentLength = 30;
    private float ropeWidth = 0.04f;

    void Start()
    {
        Vector3 ropeStartPoint = startTransform.position;

        for (int i = 0; i < segmentLength; i++)
        {
            ropeSegments.Add(new RopeSegment(ropeStartPoint));
            ropeStartPoint.y -= ropeSegLen;

            GameObject g = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            g.transform.localScale = ropeWidth * Vector3.one;
            g.transform.position = ropeStartPoint;
            g.transform.SetParent(transform);
            g.GetComponent<Renderer>().material.color = cableColor;
            linePoints.Add(g);

            if(i < segmentLength - 1) 
            {
                GameObject c = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
                c.transform.localScale = ropeWidth * Vector3.one;
                c.transform.position = ropeStartPoint;
                c.transform.SetParent(transform);
                c.GetComponent<Renderer>().material.color = cableColor;
                lineConnectors.Add(c);
            }
        }
    }




    private void FixedUpdate()
    {
        Simulate();
    }

    private void Simulate()
    {
        Vector3 forceGravity = new Vector3(0f, -1.5f, 0);

        for (int i = 0; i < segmentLength; i++)
        {
            RopeSegment firstSegment = ropeSegments[i];
            Vector3 velocity = firstSegment.posNow - firstSegment.posOld;
            firstSegment.posOld = firstSegment.posNow;
            firstSegment.posNow += velocity;
            firstSegment.posNow += forceGravity * Time.fixedDeltaTime;
            ropeSegments[i] = firstSegment;

            linePoints[i].transform.position = firstSegment.posNow;
                           
        }

        for (int i = 0; i < lineConnectors.Count; i++)
        {
            UpdateCylinderPosition(lineConnectors[i], linePoints[i + 1].transform.position, linePoints[i].transform.position);
        }


        for (int i = 0; i < 30; i++)
        {
            ApplyConstraint();
        }
    }

    private void ApplyConstraint()
    {
        RopeSegment firstSegment = ropeSegments[0];
        firstSegment.posNow = startTransform.position;
        ropeSegments[0] = firstSegment;

        RopeSegment lastSegment = ropeSegments[ropeSegments.Count - 1];
        lastSegment.posNow = endTransform.position;
        ropeSegments[ropeSegments.Count - 1] = lastSegment;



        for (int i = 0; i < segmentLength - 1; i++)
        {
            RopeSegment firstSeg = ropeSegments[i];
            RopeSegment secondSeg = ropeSegments[i + 1];

            float dist = (firstSeg.posNow - secondSeg.posNow).magnitude;
            float error = Mathf.Abs(dist - ropeSegLen);
            Vector3 changeDir = Vector3.zero;

            if (dist > ropeSegLen)
            {
                changeDir = (firstSeg.posNow - secondSeg.posNow).normalized;
            } else if (dist < ropeSegLen)
            {
                changeDir = (secondSeg.posNow - firstSeg.posNow).normalized;
            }

            Vector3 changeAmount = changeDir * error * stiffness;
            if (i != 0)
            {
                firstSeg.posNow -= changeAmount * 0.5f;
                ropeSegments[i] = firstSeg;
                secondSeg.posNow += changeAmount * 0.5f;
                ropeSegments[i + 1] = secondSeg;
            }
            else
            {
                secondSeg.posNow += changeAmount;
                ropeSegments[i + 1] = secondSeg;
            }
        }
    }



    private void UpdateCylinderPosition(GameObject cylinder, Vector3 beginPoint, Vector3 endPoint)
    {
        Vector3 offset = endPoint - beginPoint;
        Vector3 position = beginPoint + (offset / 2.0f);

        cylinder.transform.position = position;
        cylinder.transform.up = offset;
        Vector3 localScale = cylinder.transform.localScale;
        localScale.y = offset.magnitude/2;
        cylinder.transform.localScale = localScale;
    }

    public struct RopeSegment
    {
        public Vector3 posNow;
        public Vector3 posOld;

        public RopeSegment(Vector3 pos)
        {
            this.posNow = pos;
            this.posOld = pos;
        }
    }
}