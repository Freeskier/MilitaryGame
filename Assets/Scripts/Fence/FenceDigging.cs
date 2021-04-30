using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FenceDigging : MonoBehaviour
{
    TerrainDeformation TerrainDeformation;
    private Animator animator;
    public Material avaiableMaterial;
    [HideInInspector] 
    public bool segmentFinished = false;
    public float diggingDepth = 0.01f;

    
    void Start()
    {
        TerrainDeformation = FindObjectOfType<TerrainDeformation>();
        animator = Camera.main.gameObject.GetComponent<Animator>();
    }


    public void DiggingWithPoint(Vector3 point) 
    {
        if(!segmentFinished)
        {
            TerrainDeformation.LowerTerrainFromPoint(point, diggingDepth);
            GetComponent<Renderer>().material = avaiableMaterial;
            animator.SetTrigger("UseShovel");
            segmentFinished = true;
        }
    }
}
