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
    private int score;
    public Text scoreText;
    // Start is called before the first frame update
    void Start(){
        score = 0;  
        SetScoreText();
        Transform startCan = Instantiate(can, transform.position, transform.rotation);
        startCan.GetComponent<Rigidbody>().AddForce(TowardsPlayer(player)*-force);

    }

    // Update is called once per frame
    void Update(){

    } 

    void SetScoreText(){
        scoreText.text = "Score: " + score.ToString();
    }
    void IncrementScore(){
        score += 1;
        scoreText.text = "Score: " + score.ToString();
    }

    Vector3 TowardsPlayer(GameObject player){
        Vector3 direction = transform.position- player.transform.position;
        return direction;
    }

    void SpawnCan(){
        Transform canClone = Instantiate(can, transform.position, transform.rotation);
        canClone.GetComponent<Rigidbody>().AddForce(TowardsPlayer(player)*-force);
    }
}
