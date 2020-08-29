using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static int gameFlag = 0;


    public static bool animalMoving;

    public static bool buttonFinish = false;
    public GameObject[] animalArray;

    [SerializeField]
    public  GameObject[] A_Animal;
    [SerializeField]
    public  GameObject[] B_Animal;

    public static List<Animals> animList=new List<Animals>();

    //public static List<Vector2> terrainList = new List<Vector2>();
    //public static List<Terr> terrainListOccupied = new List<Terr>();
    public static Dictionary<Vector2, bool> myTerrainList = new Dictionary<Vector2, bool>();

    public static Vector2 tempUV_A;
    public static Vector2 tempUV_B;

    public static Vector2 tempTargetUV_A;
    public static Vector2 tempTargetUV_B;

    public static int indexOfGroup = 0;
    void Start()
    {
        GetAnimalsList();
        //  SetupAnimalsPos(A_Animal);
        //  SetupAnimalsPos(B_Animal);
        
    }
    void Update()
    {

    }
    public class Animals
    {
        public string Num;
        public string Name;
        public GameObject animalObj;
    }
    public class Terr
    {
        public Vector2 v2;
        public int o;
    }
    void GetAnimalsList()
    {
        foreach (GameObject child in animalArray)
        {

            Animals childAnimal = new Animals();
            childAnimal.Num = child.name.Substring(1, 3);
           

            childAnimal.Name = child.name.Remove(0,9);
            childAnimal.animalObj = child;

            animList.Add(childAnimal);
        }
    }
   
}
