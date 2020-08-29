using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class InitializeAnimal : MonoBehaviour
{
      Vector3 O_Pos;
      public int AnimalLevel;

    float scaleIndex=0.1f;

    [ContextMenu("Initialize/SetUpAnimalsPos", false, 1)]

    private void Start()
    {
        scaleIndex = Random.Range(-0.15f, 0.15f);
        transform.localScale *= (1+scaleIndex);
    }
    void SetUpAnimalsPos() {
        O_Pos = this.transform.localPosition;
        //print(this.name);
        char i = this.transform.name[1];

        if (i.ToString()== "0")
        {
            print(this.name);
            this.transform.localPosition = new Vector3(0.5f, 1f, 0.5f);
        }
        else if (i.ToString() == "1")
        {
            print(this.name);
            this.transform.localPosition = new Vector3(0.5f, 0.5f, 0.5f);
        }
        else if (i.ToString() == "2")
        {
            print(this.name);
            this.transform.localPosition = new Vector3(0.5f, 1.5f, 0.5f);
        }
    }
    [ContextMenu("Initialize/OriginalPos",false,2)]
    void OriginalPos()
    {
        this.transform.localPosition = O_Pos;
    }

}
