using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenButtons : MonoBehaviour
{
    public GameObject prefabButton;
    public int num = 9;
    public GameObject frame;

    //[SerializeField]
    Vector3[] genTransform;

    float scale;

    public static Transform FirstSign;
    public static Transform EndSign;
    void Start()
    {
        genTransform = new Vector3[num];

        scale = prefabButton.transform.localScale.x;
       
    }
    private void Update()
    {
        if ((!GameController.buttonFinish)&&(GameController.gameFlag==0))
        {
            frame.SetActive(true);
            for (int i = 0; i < genTransform.Length; i++)
            {
                genTransform[i] = new Vector3((i+1) * scale * -1, 0, 0);
               // print("genTransform["+i+"]"+genTransform[i]);
                InstantiateButton(prefabButton, genTransform[i], i+1);
               // print("transform"+genTransform[i]);
            }
            GameController.buttonFinish = true;
        }
    }
    void InstantiateButton(GameObject prefab, Vector3 pos,int index)
    {
        GameObject thisButton= Instantiate(prefab, this.transform,false);
        thisButton.GetComponent<RectTransform>().anchoredPosition = pos;
       // print("pos"+pos);
        thisButton.name = "Cube" + index;
        if (index == 1)
        {
            FirstSign = thisButton.transform;
        }
        else if (index == 9)
        {
            EndSign = thisButton.transform;
        }
     
    }
}
