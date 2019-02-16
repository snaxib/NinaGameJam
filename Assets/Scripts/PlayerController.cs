using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{ 
    public float speed;
    public float speedRot;
    public float forceApplied;
    private bool kicking = false;
    private float animationDuration = 1;
    void Update() {
        Vector3 pos = transform.position;
        if (Input.GetKey ("w")) {
            pos += transform.rotation * Vector3.forward * speed;
        }
        if (Input.GetKey ("s")) {
            pos += transform.rotation * -Vector3.forward * speed;
        }
        if (Input.GetKey ("d")) {
            transform.Rotate( Vector3.up,  Time.deltaTime * speedRot);
        }
        if (Input.GetKey ("a")) {
            transform.Rotate( -Vector3.up,  Time.deltaTime * speedRot);
        }
        if (Input.GetKey(KeyCode.Space)){
           Kick();
        }
        transform.position = pos;
    }
    void OnCollisionEnter(UnityEngine.Collision other) {
        if (other.gameObject.CompareTag ("Can") && kicking == true){
            other.gameObject.GetComponent<Rigidbody>().AddForce (0.0f, forceApplied, forceApplied);
        }
    }
    private void Kick(){
        if (!kicking){
            kicking = true;
            Invoke("StopKicking", animationDuration);
        }
    }
    private void StopKicking(){
        kicking = false;
    }
    
}