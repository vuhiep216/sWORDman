using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    private Rigidbody2D myBody;

    public float move_Speed=2f;

    public float normal_Push=10f;
    public float extra_Push =14f;

    private bool initial_Push;
    private int push_Count;
    private bool player_Died;

    public bool faceright = true;

    public gamemaster gm;

    public Joystick joystick;

    //swipe


    // Start is called before the first frame update
    void Awake()
    {
        myBody=GetComponent<Rigidbody2D>();
        gm= GameObject.FindGameObjectWithTag("gamemaster").GetComponent<gamemaster>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();

        
          float h = joystick.Horizontal;
        
        
        if (h>0 && !faceright)
        {
            Flip();
        }
 
        if (h < 0 && faceright)
        {
            Flip();
        }
    }

    void Move(){

        if(player_Died)
        return;

        if(joystick.Horizontal>0){
            myBody.velocity=new Vector2(move_Speed,myBody.velocity.y);
        } else if(joystick.Horizontal<0){
            myBody.velocity=new Vector2(-move_Speed,myBody.velocity.y);
        }
    }//Player movement

    void OnTriggerEnter2D(Collider2D target){
        
        if(player_Died)
        return;

        if(target.tag=="ExtraPush"){
            if(!initial_Push){
                initial_Push=true;
                myBody.velocity=new Vector2(myBody.velocity.x,18f);
                target.gameObject.SetActive(false);

                SoundManager.instance.JumpSoundFX();

                //exit from the on trigger enter because of initial push
                return;
            }//inotial push
            //outside of the initial push
        }//because of the initial push
        if(target.tag=="NormalPush"){

            myBody.velocity=new Vector2(myBody.velocity.x, normal_Push);

            target.gameObject.SetActive(false);

            push_Count++;

            SoundManager.instance.JumpSoundFX();

        }

        if(target.tag=="ExtraPush"){

            myBody.velocity=new Vector2(myBody.velocity.x, extra_Push);

            target.gameObject.SetActive(false);

            push_Count++;

            SoundManager.instance.JumpSoundFX();

        }

        if(push_Count == 2){
            push_Count=0;
            PlatformSpawner.instance.SpawnPlatforms();
        }
        if(target.tag=="FallDown"|| target.tag=="Bird"){
            player_Died=true;
            SoundManager.instance.GameOverSoundFX();

            GameManager.instance.Restartgame();
        }

        if(target.CompareTag("NormalPush")){
            Destroy(target.gameObject);
            gm.points +=1;
        }

        if(PlayerPrefs.GetInt("highscore")< gm.points)
        PlayerPrefs.SetInt("highscore", gm.points);
    }//on trigger enter


    public void Flip()
    {
        faceright = !faceright;
        Vector3 Scale;
        Scale = transform.localScale;
        Scale.x *= -1;
        transform.localScale = Scale;
    }

    //swipe

}
