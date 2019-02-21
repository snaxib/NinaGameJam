using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{ 
    Animator anim;
    public Animator collisionAnim;
    public CapsuleCollider[] leg;
    //Component[] leg;
    public float speed;
    public float speedRot;
    public float forceApplied;
    private bool kicking = false;
    private float animationDuration = 2.33f;
    private float startToKick = .95f;
    string clipName;
    AnimatorClipInfo[] currentClipInfo;

    float m_CurrentClipLength;
    private void Start() {
        //leg = GetComponentsInChildren<CapsuleCollider>(); // handled this in the public variable assignment because we want to keep the player's capsule collider active
        anim = GetComponent<Animator>();
    }
    void Update() {
        currentClipInfo = anim.GetCurrentAnimatorClipInfo(0);
        clipName = currentClipInfo[0].clip.name;
        Vector3 pos = transform.position;

		if (!AreWeKicking())
		{
        	if (Input.GetKey ("w")) {
				SetAnimState(1); // set animation to walk
                pos += transform.rotation * Vector3.forward * speed;
			}
        	if (Input.GetKey ("s")) {
				SetAnimState(1); // set animation to walk
                pos += transform.rotation * -Vector3.forward * speed;
        	}
        	if (Input.GetKey ("d")) {
				SetAnimState(1); // set animation to walk
               	transform.Rotate( Vector3.up,  Time.deltaTime * speedRot);
        	}
        	if (Input.GetKey ("a")) {
				SetAnimState(1); // set animation to walk
            	transform.Rotate( -Vector3.up,  Time.deltaTime * speedRot);
        	}

			if (Input.GetKey(KeyCode.Space)){
				Kick();
			}
        
        }
        transform.position = pos;
    }
    void LateUpdate() {
        if (!Input.GetKey("w") && !Input.GetKey("a") && !Input.GetKey("s") && !Input.GetKey("d") && !Input.GetKey(KeyCode.Space) && kicking == false){
			SetAnimState(0); // set animation to idle
        }    
    }
    void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag ("Can") && kicking == true){
            Debug.Log("Poop");
            other.gameObject.GetComponent<Rigidbody>().AddForce ((transform.forward+transform.up)*forceApplied);
            other.gameObject.GetComponent<Rigidbody>().AddTorque(transform.up*(forceApplied/4));
            other.gameObject.GetComponent<Rigidbody>().AddTorque(transform.right*(forceApplied));
            
        }
    }
    private void Kick(){
        if (!kicking){
            kicking = true;
			SetAnimState(2); // set animation to a kick
            Debug.Log("Kicking Started");
            Invoke("TurnOnKickCollider", startToKick);
            Invoke("StopKicking", animationDuration);
        }
    }

    // we turn on the kick collider 1 second into the kicking animation so early swings back dont do weird things to the can (assuming we didnt want that)
    private void TurnOnKickCollider(){
        foreach (CapsuleCollider collider in leg)
        {
            collider.enabled = true;
        }
    }

    private void StopKicking(){
        kicking = false;
        foreach (CapsuleCollider collider in leg){
                collider.enabled = false;
        }
		SetAnimState(0); // set animation to idle
        Debug.Log("Kicking Stopped");
    }
    
	private void SetAnimState(int newState)
	{
		if (newState == 2) // if we are kicking, pull a random kick
			anim.SetInteger("randomKick", Random.Range(0, 3));
				
		anim.SetInteger("characterState", newState);
        collisionAnim.SetInteger("characterState", newState);
    }

	private bool AreWeKicking()
	{
		if (anim.GetInteger("characterState") == 2 || anim.GetInteger("characterState") == 3)
		{
			return true;
		}
		return false;
	}
}