using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InitializeButton : MonoBehaviour
{   [Space]
    [Header("Set Up Cube Position")]
    Vector3 StartPos;
    [HideInInspector]
    public Transform thisObj;

    [HideInInspector]
    public Vector3 StartSignPos;

    public Transform StartSign;
    public Transform StopSign;
    [Range(1,10)]
    public float speed;
    

    [Space]
    [Header("Generate and Update Animals")]
    public bool GenAnim=false;
    public int count;
    public int intOfAnim;

    [HideInInspector]
    public GameObject currentAnim;

    [Space]
    [Header("Generate Animals on Terrains")]
   // public static int index;

    bool finishInitiate = false;
    void Start()
    {
       // gameController = GameObject.Find("gameController");
    }

    // Update is called once per frame
    void Update()
    {
        if (GameController.buttonFinish && (!finishInitiate))
        {
            InitiatePos();
        }
        if (finishInitiate)
        {
            if (!GenAnim)
            {
              GenAnimal();
            } 
            if (GameController.gameFlag == 0)
            {
                if (transform.GetComponent<RectTransform>().anchoredPosition.x >= StartPos.x)
                {
                   
                    StartSignPos = new Vector3(StartSignPos.x - speed, StartSignPos.y, StartSignPos.z);
                    transform.GetComponent<RectTransform>().Rotate(new Vector3(0, 5, 0));
                    //print("InitializeButton"+StartSignPos);
                }
                else
                {
                    transform.GetComponent<RectTransform>().rotation = Quaternion.identity;
                    if (transform.name == StopSign.name)
                    {
                        GameController.gameFlag = 1;
                    }
                }
                transform.GetComponent<RectTransform>().anchoredPosition = new Vector3(StartSignPos.x,StartSignPos.y,StartSignPos.z);
            }

            // print(GameController.gameFlag);
            DeleteAnimal();
        }
       
    }
   void InitiatePos()
    {
        StartSign = GenButtons.FirstSign;
        StopSign = GenButtons.EndSign;
        //thisObj = this.transform;
        
        StartPos = this.transform.GetComponent<RectTransform>().anchoredPosition;
        StartSignPos = StartSign.GetComponent<RectTransform>().anchoredPosition;
        finishInitiate = true;
    }
    void GenAnimal()
    {
        count =  GameController.animList.Count;
        intOfAnim = Random.Range(0, count);
        string animalName = GameController.animList[intOfAnim].Name;
        
        Transform tempTransform = GameController.animList[intOfAnim].animalObj.transform;
        
        currentAnim = Instantiate(GameController.animList[intOfAnim].animalObj,transform.position,Quaternion.identity);
        currentAnim.transform.SetParent(this.transform);

        currentAnim.transform.localPosition = tempTransform.localPosition;
        currentAnim.transform.localRotation = tempTransform.localRotation;
        currentAnim.transform.localScale = tempTransform.localScale;

       // print("Index in button:"+ intOfAnim + animalName);
       
        GenAnim = true;
       
        ButtonController.selectingName = null;
       
    }
    public void DeleteAnimal()
    {
        if (ButtonController.selectingName != null && ButtonController.isSelectingTerrain)
        {
           // print("AnimalHere"+ButtonController.GetNameForTerrainSelection.name+":"+ButtonController.GetNameForTerrainSelection.GetComponent<TerrainData>().animalHere);
          
               if(ButtonController.GetNameForTerrainSelection.GetComponent<TerrainData>().animalHere)
               {
                    //print("GetNameForTerrainSelection: " + ButtonController.GetNameForTerrainSelection);
                    Destroy(currentAnim);
                    GenAnim = false;
               }

            
        }
        
    }
}
