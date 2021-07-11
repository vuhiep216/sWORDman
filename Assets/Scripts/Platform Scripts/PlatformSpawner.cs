using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    public static PlatformSpawner instance;

    [SerializeField]
    private GameObject left_Platform, right_Platform;
    // Start is called before the first frame update

    private float left_X_Min = -1.5f, left_X_Max = -2.8f, right_X_Min=1.8f,right_X_Max=2.8f;
    private float y_Treshold = 2.6f;
    private float last_Y;

    public int spawn_Count =8;
    private int platform_Spawned;

    [SerializeField]
    private Transform platform_Parent;

    //Spawn enemy
    [SerializeField]
    private GameObject bird;
    public float bird_Y=5f;
    private float bird_X_Min=-2.3f, bird_X_Max=2.3f;

    void Awake()
    {
        if(instance ==null)
            instance=this;
    }

    void Start(){
        last_Y=transform.position.y;

        SpawnPlatforms();
    }

    public void SpawnPlatforms(){
        Vector2 temp=Vector2.zero;
        GameObject newPlatform=null;
        for(int i=0;i<spawn_Count;i++){
            temp.y =last_Y;
            //have even number
            if((platform_Spawned % 2)==0){
                temp.x=Random.Range(left_X_Min,left_X_Max);

                newPlatform=Instantiate(right_Platform,temp,Quaternion.identity);
            } else{
                //if had odd number
                temp.x =Random.Range(right_X_Min,right_X_Max);
                newPlatform=Instantiate(left_Platform, temp, Quaternion.identity);
            }

            newPlatform.transform.parent=platform_Parent;


            last_Y +=y_Treshold;
            platform_Spawned++;
        }

        if(Random.Range(0,2)>0){
            SpawnBird();
        }
    }//spawn platforms
    void SpawnBird(){
        Vector2 temp =transform.position;
        temp.x=Random.Range(bird_X_Min, bird_X_Max);
        temp.y+=bird_Y;

        GameObject newBird = Instantiate(bird, temp,Quaternion.identity);
        newBird.transform.parent=platform_Parent;
    }

}//class
