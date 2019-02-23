using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanController : MonoBehaviour
{
    AudioSource kickPlayer;
    AudioSource dropPlayer;
    public AudioClip kick;
    public AudioClip drop;

     void Start ()   
     {
        kickPlayer = AddAudio(false, false, 0.15f);
        dropPlayer = AddAudio(false, false, 0.15f);
        kickPlayer.clip = kick;
        dropPlayer.clip = drop;
     }        
 
     void OnTriggerEnter ()  //Plays Sound Whenever collision detected
     {
        Debug.Log("Playing Kick");
        kickPlayer.Play();
     }

     private void OnCollisionEnter(){
         dropPlayer.Play();
     }
    public AudioSource AddAudio(bool loop, bool playAwake, float vol){ 
        AudioSource newAudio = gameObject.AddComponent<AudioSource>();
        //newAudio.clip = clip; 
        newAudio.loop = loop;
        newAudio.playOnAwake = playAwake;
        newAudio.volume = vol; 
        return newAudio; 
    }

}
