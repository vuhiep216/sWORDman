using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    [SerializeField]
    private AudioSource soundFX;

    [SerializeField]
    private AudioClip jumpClip, gameOverClip;
    // Start is called before the first frame update
    void Awake()
    {
        if(instance == null)
        instance=this;
    }

    public void JumpSoundFX(){
        soundFX.clip=jumpClip;
        soundFX.Play();
    }

    public void GameOverSoundFX(){
        soundFX.clip=gameOverClip;
        soundFX.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}//class
