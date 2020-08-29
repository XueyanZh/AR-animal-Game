using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateTerrains : MonoBehaviour
{
    public int scaleSet = 2;
   // public static int scale = 4;

    public List<Vector2> uv;
    Vector2 pos;
    public List<string> Num;
    string num;

    public GameObject prefab;
    [ContextMenu("Initialize/getPos", false, 0)]
    public void getPosArray() {
        uv = new List<Vector2>();
        Num = new List<string>();
        for (int i=0; i < scaleSet;i++)
        { 
            for(int n = 0; n < scaleSet; n++)
            {
                pos= new Vector2((i*0.1f), (n*0.1f));
                uv.Add(pos);
                //print(m);
                num = (i + 1).ToString() + (n + 1).ToString();
                Num.Add(num);
            }
        }
    }
    [ContextMenu("Initialize/GenTerrain", false, 1)]
    public void GenerateTerrain()
    {
        for (int x = 0; x < uv.Count; x++)
        {
            print(uv[x]);
            GameObject Terrain= Instantiate(prefab, new Vector3(uv[x].x, 0, uv[x].y), Quaternion.identity);
            Terrain.GetComponent<TerrainData>().uv = new Vector2(uv[x].x*10+1,uv[x].y*10+1);
            Terrain.name = "Terrain" + Num[x];
            print(Num[x]);
        }
    }
}
