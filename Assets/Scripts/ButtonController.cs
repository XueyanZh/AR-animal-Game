using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    public Camera cam;
    public Camera mainCam;
    public RectTransform cav;
 //   public Material mat;
    public GameObject gameController;

    //* ray to the cube
    Ray ray;
    RaycastHit hit;

    //* ray to the terrain
    Ray rayTerrain;
    RaycastHit hitTerrain;

    //* the transform of selected cube and terrain.
    Transform selection;
    public static Transform terrainSelection;

    //* the original cube pos and material
  //  Material matOriginal;
    Vector3 posOriginal;
    //*pos on screen
    Vector2 pos;

    //* the cube selected
    public static string selectingName;
    //* the bool of if selecting any terrain.
    public static bool isSelectingTerrain = false;

    public static Transform GetNameForTerrainSelection;

    public int index;
    GameObject Animal;
    Vector3 AnimalPos;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (GameController.gameFlag == 1)
        {
            if (selection == null)
            {
                Selection();
            }
            else
            {
                if (selection.tag == "UI")
                {
                    RectTransformUtility.ScreenPointToLocalPointInRectangle(cav, new Vector2(Input.mousePosition.x, Input.mousePosition.y), cam, out pos);
                    selection.localPosition = new Vector3(pos.x, pos.y, 0.001f);

                    if (Input.GetMouseButtonUp(0))
                    {
                        Release();
                    }
                }
            }
        }
    }
    public void Selection()
    {//single call
        if (Input.GetMouseButton(0))
        {
            ray = cam.ScreenPointToRay(Input.mousePosition);
            Physics.Raycast(ray, out hit, 100f);
            selection = hit.transform;
            if (selection != null)
            {
              //  matOriginal = selection.GetComponent<MeshRenderer>().material;
                posOriginal = selection.position;
                if (selection.tag == "UI")
                {
                 //   selection.GetComponent<MeshRenderer>().material = mat;

                    //**get index of animals here**//
                    index = selection.GetComponent<InitializeButton>().intOfAnim;
                }
                else
                {
                    selection = null;
                }
            }
           // print("animal: " + index);
        }
    }
    public void Release()
    {//single call
       // selection.GetComponent<MeshRenderer>().material = matOriginal;
        selection.GetComponent<Transform>().position = posOriginal;

        selectingName = selection.name;
        isSelectingTerrain = false;
        //print(selectingName);

        selection = null;

      
            RayToTerrain();
       
            ReleaseTerrain();
       
       
    }
    public void RayToTerrain()
    {//single call
        rayTerrain = mainCam.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(rayTerrain, out hitTerrain, 1000f);
        terrainSelection = hitTerrain.transform;
       
    }
    public void ReleaseTerrain()
    {//single call
        if (terrainSelection != null)
        {
            if (terrainSelection.tag == "Terrains")
            {
                if (!terrainSelection.GetComponent<TerrainData>().animalHere)
                {
                    if (index == terrainSelection.GetComponent<TerrainData>().thisTerrain.z - 1||index==2)
                    { 
                        isSelectingTerrain = true;

                     //   terrainSelection.GetComponent<MeshRenderer>().material = mat;
                        
                        AnimalPos = gameController.GetComponent<GameController>().A_Animal[index].transform.position;
                       // print(AnimalPos + ": "+index);
                        Animal = gameController.GetComponent<GameController>().A_Animal[index].gameObject;
                        //Instantiate(gameController.GetComponent<GameController>().A_Animal[index].gameObject, terrainSelection, false);
                        GameObject AnimalObj = Instantiate(Animal,AnimalPos,Quaternion.identity);
                        AnimalObj.name = Animal.name +"_"+ terrainSelection.GetComponent<TerrainData>().u + terrainSelection.GetComponent<TerrainData>().v;
                        AnimalObj.transform.localPosition *= 0.1f;
                        AnimalObj.transform.localPosition += terrainSelection.localPosition;


                        //**** Turn animal false to true *****//
                        AnimalObj.GetComponent<AnimalMoving>().beforeMove = true;
                        terrainSelection.GetComponent<TerrainData>().animalHere = true;
                        GetNameForTerrainSelection = terrainSelection;//** marked the selected terrain **//        
                        //print(GetNameForTerrainSelection);
                    }
                }
            }
          //  terrainSelection.GetComponent<MeshRenderer>().material = matOriginal;
        }
        
       
        terrainSelection = null;
    }
    

}
