using UnityEngine;
using System.Collections;

public class TerrainDeformation : MonoBehaviour
{
    public Terrain myTerrain;
    public Terrain originalTerrain;
    public float diggingMultiplier = 0.01f;
    public float diggingDepth = .01f;
    public int RangeMultiplier = 4;
    int xResolution;
    int zResolution;
    float[,] heights;
    float[,] originalHeights;
    public LayerMask layerMask;
    [System.NonSerialized]
    public bool canDig = false;

    [System.NonSerialized]
    public float highestPoint;


    void Start()
    {
        xResolution = myTerrain.terrainData.heightmapResolution;
        zResolution = myTerrain.terrainData.heightmapResolution;
        heights = myTerrain.terrainData.GetHeights(0, 0, xResolution, zResolution);
        originalHeights = originalTerrain.terrainData.GetHeights(0, 0, xResolution, zResolution);
        myTerrain.terrainData.SetHeights(0, 0, originalHeights);
    }



    public void LowerTerrain(Vector3 point, float lowerHeight)
    {
        int terX = (int)((point.x / myTerrain.terrainData.size.x) * xResolution);
        int terZ = (int)((point.z / myTerrain.terrainData.size.z) * zResolution);
        int h = (int)myTerrain.terrainData.size.y;

        float[,] height = myTerrain.terrainData.GetHeights(terX - RangeMultiplier, terZ - RangeMultiplier, RangeMultiplier * 2 + 1, RangeMultiplier * 2 + 1);  //new float[1,1];

        float minPoint = height[0, 0];

        for (int tempY = 0; tempY < RangeMultiplier * 2 + 1; tempY++)
            for (int tempX = 0; tempX < RangeMultiplier * 2 + 1; tempX++)
            {
                if (height[tempX, tempY] < minPoint) minPoint = height[tempX, tempY];
            }
            
        for (int tempY = 0; tempY < RangeMultiplier * 2 + 1; tempY++)
            for (int tempX = 0; tempX < RangeMultiplier * 2 + 1; tempX++)
            {
                if (Mathf.Pow(RangeMultiplier - tempX, 2) + Mathf.Pow(RangeMultiplier - tempY, 2) <= Mathf.Pow(RangeMultiplier, 2))
                {
                    

                    //if (height[tempX, tempY] >= lowerHeight)
                    {
                        //height[tempX, tempY] = lowerHeight / h - .001f;
                        float dist_to_target = Mathf.Pow((float)tempY - RangeMultiplier, 2) + Mathf.Pow((float)tempX - RangeMultiplier, 2);
                        float maxDist = RangeMultiplier * 2;
                        
                        float proportion = Mathf.Sqrt (dist_to_target) / maxDist;

                        //Debug.Log(height[tempX,tempY] + " " +  lowerHeight + " " + lowerHeight / h);
                        if (height[tempX,tempY] - lowerHeight/h * (1f - proportion) * diggingDepth > lowerHeight/h ) 
                            height[tempX,tempY] -= lowerHeight / h * (1f - proportion) * diggingDepth;
                        else
                            height[tempX, tempY] = lowerHeight / h - .001f;
                    }
                        
                }
            }

        myTerrain.terrainData.SetHeights(terX - RangeMultiplier, terZ - RangeMultiplier, height);
    }

    public void LowerTerrainFromPoint(Vector3 point, float lowerHeight)
    {
        int terX = (int)((point.x / myTerrain.terrainData.size.x) * xResolution);
        int terZ = (int)((point.z / myTerrain.terrainData.size.z) * zResolution);
        int h = (int)myTerrain.terrainData.size.y;

        float[,] height = myTerrain.terrainData.GetHeights(terX - 1 , terZ - 1, 3, 3);  //new float[1,1];

        for(int i = 0; i < 3; i++) {
            for (int j = 0; j < 3; j++)
            {
                height[i,j] -= lowerHeight;
            }
        }
                    
        myTerrain.terrainData.SetHeights(terX -1, terZ -1, height);
    }



    public bool IsDiggingCompletedInArea(Vector3 Dimension, Vector3 Offset, Vector3 Position)
    {
        int terX = (int)(((Position.x + Offset.x) / myTerrain.terrainData.size.x) * xResolution);
        int terZ = (int)(((Position.z + Offset.z) / myTerrain.terrainData.size.z) * zResolution);

        float[,] height = myTerrain.terrainData.GetHeights(terX - (int)Dimension.x, terZ - (int)Dimension.z, (int)Dimension.x * 2 + 1, (int)Dimension.z * 2 + 1);
        int h = (int)myTerrain.terrainData.size.y;

        bool check = true;
        for (int i = 0; i < (int)Dimension.x * 2 + 1; i++)
        {
            for (int j = 0; j < (int)Dimension.z * 2 + 1; j++)
            {
                if (height[j, i] > ((Position.y - Dimension.y / 2) / h)) check = false;
            }
        }
        return check;     
    }

    // public float[,] GetMinPoint() {
    //     int terX = (int)((point.x / myTerrain.terrainData.size.x) * xResolution);
    //     int terZ = (int)((point.z / myTerrain.terrainData.size.z) * zResolution);

    //     float[,] height = myTerrain.terrainData.GetHeights(terX - RangeMultiplier, terZ - RangeMultiplier, RangeMultiplier * 2 + 1, RangeMultiplier * 2 + 1);  //new float[1,1];

    //     float minPoint = height[0, 0];

    //     for (int tempY = 0; tempY < RangeMultiplier * 2 + 1; tempY++)
    //         for (int tempX = 0; tempX < RangeMultiplier * 2 + 1; tempX++)
    //         {
    //             if (height[tempX, tempY] < minPoint) minPoint = height[tempX, tempY];
    //         }
    // }
}