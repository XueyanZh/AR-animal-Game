using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroupAnimal : MonoBehaviour
{   int i; 
    void awake() 
    {
        int r = Random.Range(0,2);
        if (r == 0)
        {
            i = 0;
        }
        else
        {
            i = 1;
        }
    }

    private void Start()
    {
        print(this.name+" -test:" + GetComponentsInChildren<AnimalMoving>()[0].UV);
        print(this.name+" -test:" + GetComponentsInChildren<AnimalMoving>()[1].UV);
        GameObject ter;
        if(i == 0)
        {
            ter=GameObject.Find("Terrain" + (int)GetComponentsInChildren<AnimalMoving>()[0].UV.x + (int)GetComponentsInChildren<AnimalMoving>()[0].UV.y);
        }
        else
        {
            ter= GameObject.Find("Terrain" + (int)GetComponentsInChildren<AnimalMoving>()[1].UV.x + (int)GetComponentsInChildren<AnimalMoving>()[1].UV.y);
        }
        ter.GetComponent<TerrainData>().animalGroupHere = true;
    }

}
