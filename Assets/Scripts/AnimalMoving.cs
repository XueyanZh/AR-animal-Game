using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using System;
public class AnimalMoving : MonoBehaviour
{
   
    public Vector2 UV;
    Vector2 targetUV;
  
    public bool beforeMove;
    bool ifFirstStart = true;
    public bool stop = false;
    public int interactStatus = 0;
    
    float timer = 0;
    float timeFlagTemp;

    float timeFlag;//when stop
    bool flagBool = false;//
    float stopTimeFlag;//how long stop
    bool stopFlagBool = true;//
    bool stopping=false;//stop timer

    string eventStatus;
    GameObject eventTarget;

    float speed = 0.0005f;
    float dis = 0;

    public GameObject particleEffect_L;
    public GameObject particleEffect_F;
    //public Material mat;
    //public Material originalMat;
    //public Material direction;

    public GameObject prefabGroup;
    public bool isTrigger=false;
    public string nameOfGroup="";

    Vector4 dir4 = new Vector4(0,0,0,0);
    Vector4 flag4 = new Vector4(1, 1, 1, 1);
    int dir = 0;
    private void Start()
    {
        targetUV = new Vector2(0, 0);
       
    }
   
    void Update()
    {
        if (interactStatus == 0)
        {   
            if(dir4!=flag4)
            {
                RandomMove();
          
                if (!stop)
                {
                    if (beforeMove)
                    {
                        if (ifFirstStart)
                        {
                            UV = GetOriginalUV();
                            ifFirstStart = false;
                        }
                        else
                        {
                            targetUV = DetectNextUV(transform.name,dir4,dir);
                            if(targetUV!=new Vector2(0, 0))
                            turnTo(targetUV);
                        }
                    }

                    else if (!beforeMove)
                    {
                        transform.position += transform.forward * speed;
                        dis += speed;

                        if (dis >= 0.1)
                        {
                            UpdateTerrainData(UV, targetUV);
                            //GameObject.Find("Terrain" + ((int)targetUV.x).ToString() + ((int)targetUV.y).ToString()).GetComponent<MeshRenderer>().material=mat;
                            UV = targetUV;
                      
                            // GameObject.Find("Terrain" + ((int)UV.x).ToString() + ((int)UV.y).ToString()).GetComponent<MeshRenderer>().material = originalMat;
                            dis = 0;
                            beforeMove = true;
                        }
                    }
                }
            }
        }

        else if (interactStatus == 1)
        {
            EventStatus();
        }

    }
    Vector2 DetectNextUV(string thisName, Vector4 dir4, int dir)
    {
        //print("flag:"+dir4);
        Vector2 output = new Vector2(0, 0);
        if (dir4 == flag4)
        {
            print("dir4 = 1111");
            return output;
        }
        else
        {
            print("dir in detect = " + dir);
            print("dir4 != 1111");
            Vector2 moveUV;
            Vector2 nextUV = new Vector2();
            dir = Random.Range(1, 5);

            if (dir == 1)
            {
                dir4.x = 1;
                moveUV = new Vector2(0, 1);
            }
            else if (dir == 2)
            {
                moveUV = new Vector2(1, 0);
                dir4.y = 1;
            }
            else if (dir == 3)
            {
                moveUV = new Vector2(0, -1);
                dir4.z = 1;
            }
            else
            {
                moveUV = new Vector2(-1, 0);
                dir4.w = 1;
            }
            nextUV = UV + moveUV;

            GameObject tarUV = GameObject.Find("Terrain" + (nextUV.x).ToString() + (nextUV.y).ToString());

            //if ((nextUV.x > GenerateTerrains.scale || nextUV.x < 1) || (nextUV.y > GenerateTerrains.scale || nextUV.y < 1))
            //{
            //    return DetectNextUV(thisName);
            //}
            //else 

            if (tarUV == null)
            {
                return DetectNextUV(thisName, dir4, dir);
            }
            else
            {
               // print("nextUV:" + nextUV);
                if (tarUV.GetComponent<TerrainData>().thisTerrain.z - 1 == int.Parse(thisName[1].ToString()))
                {//Terrain checked
                    if (tarUV.GetComponent<TerrainData>().animalGroupHere)
                    {
                        return DetectNextUV(thisName, dir4, dir);
                    }
                    else
                    {
                        beforeMove = false;
                        return nextUV;
                    }
                }
                else
                {
                    if (int.Parse(thisName[1].ToString()) == 2)
                    {//bird checked
                        if (tarUV.GetComponent<TerrainData>().animalGroupHere)
                        {
                            return DetectNextUV(thisName, dir4, dir);
                        }
                        else
                        {
                            beforeMove = false;
                            return nextUV;
                        }
                    }
                    //terrain wrong
                    return DetectNextUV(thisName, dir4, dir);
                }
            }

        }

    }
    Vector2 GetOriginalUV()
    {
        int U;
        int V;
        U = int.Parse(gameObject.name[5].ToString());
        V = int.Parse(gameObject.name[6].ToString());
        //print("GetOriginalUV:"+gameObject.name);
        return new Vector2(U, V);
    }
    void turnTo(Vector2 inputUV)
    {
        Vector2 flag1, flag2, flag3, flag4;
        flag1 = new Vector2(0.0f, 1.0f)+UV;
        flag2 = new Vector2(-1.0f, 0.0f)+UV;
        flag3 = new Vector2(0.0f, -1.0f)+UV;
        flag4 = new Vector2(1.0f, 0.0f)+UV;
        if (inputUV == flag1)
        { 
            this.transform.rotation = Quaternion.Euler(0, 0, 0);
           // if (GameObject.Find("Terrain" + ((int)UV.x).ToString() + ((int)UV.y + 1).ToString()))
           // GameObject.Find("Terrain" + ((int)UV.x).ToString() + ((int)UV.y+1).ToString()).GetComponent<MeshRenderer>().material = direction;
        
        }
        else if (inputUV == flag2)
        {
            this.transform.rotation = Quaternion.Euler(0, -90, 0);
            //if(GameObject.Find("Terrain" + ((int)UV.x - 1).ToString() + ((int)UV.y).ToString()))
           // GameObject.Find("Terrain" + ((int)UV.x-1).ToString() + ((int)UV.y).ToString()).GetComponent<MeshRenderer>().material = direction;
        
        }
        else if (inputUV == flag3)
        {
            this.transform.rotation = Quaternion.Euler(0, 180, 0);
           // if(GameObject.Find("Terrain" + ((int)UV.x).ToString() + ((int)UV.y-1).ToString()))
           // GameObject.Find("Terrain" + ((int)UV.x).ToString() + ((int)UV.y-1).ToString()).GetComponent<MeshRenderer>().material = direction;
          
        }
        else if (inputUV == flag4)
        {
            this.transform.rotation = Quaternion.Euler(0, 90, 0);
           // if(GameObject.Find("Terrain" + ((int)UV.x +1).ToString() + ((int)UV.y).ToString()))
           // GameObject.Find("Terrain" + ((int)UV.x+1).ToString() + ((int)UV.y).ToString()).GetComponent<MeshRenderer>().material = direction;
           
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Terrains") 
        {
            eventTarget = other.gameObject;
            
            if (other.name.Substring(0,4) == this.name.Substring(0,4))
            {
               // print(other.name.Substring(0, 4));
                //substring(0,4) : get 4 characters in string from the 0 one.
                eventStatus = "love";
            }
            else
            {
                //
                eventStatus = "fight";
            }

            interactStatus = 1;

            //print("Collision:"+"UV:"+UV+"targetUV:"+targetUV);

            GameObject animalGroup;

            if (other.gameObject.GetComponent<AnimalMoving>().isTrigger == false)
            {
                animalGroup = Instantiate(prefabGroup, this.transform.position, Quaternion.identity);
                animalGroup.transform.rotation = this.transform.rotation;
                animalGroup.transform.localScale = this.transform.localScale;
                GameController.indexOfGroup += 1;
                nameOfGroup = "AnimalGroup" + GameController.indexOfGroup.ToString();
                animalGroup.name = nameOfGroup;
                isTrigger = true;
            }
            else
            {
                nameOfGroup = "AnimalGroup" + GameController.indexOfGroup.ToString();
            }
            // print("setParent" + this.name);
            // print("update Group index:" + GameController.indexOfGroup);

            animalGroup = GameObject.Find(nameOfGroup);
            this.transform.SetParent(animalGroup.transform);
            // print("setParent");

            this.GetComponent<AnimalMoving>().enabled = false;

        }
    }
    void UpdateTerrainData(Vector2 UV,Vector2 targetUV)
    {
        GameObject.Find("Terrain" + ((int)UV.x).ToString() + ((int)UV.y).ToString()).GetComponent<TerrainData>().animalHere = false;
        GameObject.Find("Terrain" + ((int)targetUV.x).ToString() + ((int)targetUV.y).ToString()).GetComponent<TerrainData>().animalHere = true;

        if (GameController.myTerrainList.ContainsKey(UV))
        {
            GameController.myTerrainList[UV] = false;
         //   print("UV:"+GameController.myTerrainList[UV]);
        }
        if (GameController.myTerrainList.ContainsKey(targetUV))
        {
            GameController.myTerrainList[targetUV] = true;
          //  print("UV:" + GameController.myTerrainList[targetUV]);
        }
    }
    void RandomMove()
    {
        if (!flagBool)
        {
           // print("flagRandomTime");
            timeFlag = Random.Range(1, 3);
            flagBool = true;
        }

        if (!stop)
        {
            timer += Time.deltaTime;
            if (timer >= timeFlag)
            {
              //  print("stop");
                timer = 0;
                stop = true;
                stopFlagBool = false;
            }
        }
       
        if (!stopFlagBool)
        { 
            stopTimeFlag = Random.Range(1, 2);
          //  print("stop for " + stopTimeFlag + "seconds");
            stopFlagBool = true;
            stopping = true;
        }

        if (stopping)
        {
            timeFlagTemp += Time.deltaTime;
            if (timeFlagTemp >= stopTimeFlag)
            {
                timeFlagTemp = 0;
            //    print("stop time");
                stop = false;
                flagBool = false;
            }
        }
    }
    void EventStatus()
    {
        if (eventStatus == "love")
        {
           // print(this.name+" UV:"+UV);
           // print(this.name + " is loving " + eventTarget.name);
            if ((int)Random.Range(0, 2) == 0)
            { 
                eventStatus = "generate";  
            }
            else
            {
             //   print("nothing generated");
                //interactStatus = 2;
            }
            
        }
        else if (eventStatus == "fight")
        {
            if (gameObject.GetComponent<InitializeAnimal>().AnimalLevel > eventTarget.gameObject.GetComponent<InitializeAnimal>().AnimalLevel)
            {
                //this win
             //   print(this.name + "win");
                //interactStatus = 2;
            }
            else
            {
                //this lose
                //interactStatus = 2;
            }
        }
        else if (eventStatus == "generate")
        {
          //  print("a new baby was generated");
            //interactStatus = 2;
        }
    }
}
