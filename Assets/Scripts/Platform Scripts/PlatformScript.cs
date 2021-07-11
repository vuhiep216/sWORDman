using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformScript : MonoBehaviour
{
    [SerializeField]
    private GameObject heart, trap;

    [SerializeField]
    private Transform spawn_Point;
    // Start is called before the first frame update
    void Start()
    {
        GameObject newHeart=null;

        if(Random.Range(0,10)>3){
            newHeart=Instantiate(heart, spawn_Point.position,Quaternion.identity);
        } else{
            newHeart=Instantiate(trap, spawn_Point.position,Quaternion.identity);
        }
        newHeart.transform.parent = transform;
    }

  
}//class
