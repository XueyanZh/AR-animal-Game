using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TerrainData : MonoBehaviour
{
    public Vector3 thisTerrain;
    public int u;
    public int v;
    //public int w;
    public Vector2 uv = new Vector2();

    public bool animalHere=false;
    //bool switchMat=false;

    public bool animalGroupHere = false;

    public Material mat;
    public Material mat2;
    private void Awake()
    {
        animalHere = false;
        thisTerrain = new Vector3(uv.x, uv.y, thisTerrain.z);
        u = (int)uv.x;
        v = (int)uv.y;

        GameController.myTerrainList.Add(uv,false);
    }
    void Start()
    {
      
    }
    private void Update()
    {
       
            if (animalHere)
            {
              //  transform.GetComponent<MeshRenderer>().material = mat;
                //switchMat = true;
            }
            else
            {
              //  transform.GetComponent<MeshRenderer>().material = mat2;
                //switchMat = true;
            }
       
      
    }

}
