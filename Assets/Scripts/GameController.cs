using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public Transform can;
    public int force;
    public GameObject player;
    public GameObject trashcan1;
    public GameObject trashcan2;
    public GameObject spawnTarget;
    private List<Transform> spawners = new List<Transform>();
    private int numSpawners;
    private int score;
    public Text scoreText;
    public Material[] mats;
    // Start is called before the first frame update
    void Start(){
        score = 0;  
        SetScoreText();
        GetChildren();
        SpawnCan();
    }
    // Update is called once per frame
    void Update(){

    } 
    void SetScoreText(){
        scoreText.text = "Score: " + score.ToString();
    }
    void IncrementScore(){
        score += 1;
        SetScoreText();
    }
    Vector3 TowardsObject(GameObject target, Transform source){
        Vector3 direction = source.position - target.transform.position;
        return direction;
    }
    void SpawnCan(bool multi = false){
        if (!multi){
            Transform spawner = PickSpawner(spawners);
            Transform canClone = Instantiate(can, spawner.position, transform.rotation);
            canClone.GetComponent<Rigidbody>().AddForce(TowardsObject(spawnTarget, spawner)*-force);
            canClone.GetComponent<Renderer>().material = mats[Random.Range(0,3)];
            canClone.GetComponent<Rigidbody>().AddTorque(transform.up*(force/4));
            canClone.GetComponent<Rigidbody>().AddTorque(transform.right*(force));
        }
        if (multi){
            for (int i = 0; i < score; i++){
                Transform spawner = PickSpawner(spawners);
                Transform canClone = Instantiate(can, spawner.position, transform.rotation);
                canClone.GetComponent<Rigidbody>().AddForce(TowardsObject(spawnTarget, spawner)*-force);
                canClone.GetComponent<Renderer>().material = mats[Random.Range(0,3)];
                canClone.GetComponent<Rigidbody>().AddTorque(transform.up*(force/4));
                canClone.GetComponent<Rigidbody>().AddTorque(transform.right*(force));
            }
        }       
    }
    Transform PickSpawner(List<Transform> spawners){
        int spawnIndex = Random.Range(0, numSpawners);
        return spawners[spawnIndex];
    }
    void GetChildren(){
        foreach (Transform child in transform)
        {
            spawners.Add(child);
            Debug.Log("added child");
        }
        numSpawners = spawners.Count;
    }
}
