using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{ 
    Animator anim;
    Component[] leg;
    public float speed;
    public float speedRot;
    public float forceApplied;
    private bool kicking = false;
    private float animationDuration = 2.1f;
    string clipName;
    AnimatorClipInfo[] currentClipInfo;

    float m_CurrentClipLength;
    private void Start() {
        leg = GetComponentsInChildren<CapsuleCollider>();
        anim = GetComponent<Animator>();
    }
    void Update() {
        currentClipInfo = anim.GetCurrentAnimatorClipInfo(0);
        clipName = currentClipInfo[0].clip.name;
        Vector3 pos = transform.position;
        if (Input.GetKey ("w")) {
            if (clipName != "kick") {
                anim.Play("walk", -1);
                pos += transform.rotation * Vector3.forward * speed;
            }
            else{    
            }
        }
        if (Input.GetKey ("s")) {
            if (clipName != "kick") {
                anim.Play("walk", -1);
                pos += transform.rotation * -Vector3.forward * speed;
            }
            else{    
            }  
        }
        if (Input.GetKey ("d")) {
            if (clipName != "kick") {
                anim.Play("walk", -1);
                transform.Rotate( Vector3.up,  Time.deltaTime * speedRot);
            }
            else{    
            }
        }
        if (Input.GetKey ("a")) {
            if (clipName != "kick") {
                anim.Play("walk", -1);
                transform.Rotate( -Vector3.up,  Time.deltaTime * speedRot);
            }
            else{    
            }
        }
        if (Input.GetKey(KeyCode.Space)){
            anim.Play("kick", -1);
            Kick();
        }
        transform.position = pos;
    }
    void LateUpdate() {
        if (!Input.GetKey("w") && !Input.GetKey("a") && !Input.GetKey("s") && !Input.GetKey("d") && !Input.GetKey(KeyCode.Space) && kicking == false){
            anim.Play("idle",-1);
        }    
    }
    void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag ("Can") && kicking == true){
            Debug.Log("Poop");
            other.gameObject.GetComponent<Rigidbody>().AddForce (0.0f, forceApplied, forceApplied);
        }
    }
    private void Kick(){
        if (!kicking){
            kicking = true;
            Debug.Log("Kicking Started");
            foreach (CapsuleCollider collider in leg){
                collider.enabled = true;
            }
            Invoke("StopKicking", animationDuration);
        }
    }
    private void StopKicking(){
        kicking = false;
        foreach (CapsuleCollider collider in leg){
                collider.enabled = false;
        }
        anim.Play("idle", -1);
        Debug.Log("Kicking Stopped");
    }
    
}