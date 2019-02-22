using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public Transform can;
    public int force;
    public GameObject player;
    public GameObject spawnTarget;
    private List<Transform> spawners = new List<Transform>();
    private int numSpawners;
    private int score;
    public Text scoreText;
    public Text timerText;
    public GameObject endGame;
    private float secondsCount;
     private int minuteCount;
     private int hourCount;
     public int endScore;
    public Material[] mats;
    // Start is called before the first frame update
    void Start(){
        endGame.SetActive(false);
        score = 0;  
        SetScoreText();
        GetChildren();
        SpawnCan();

    }
    // Update is called once per frame
    void Update(){
        if (score < endScore){
            UpdateTimerUI();
        }
        else if (score >= endScore){
            StartCoroutine( gameEnd() );
        }
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
    public void UpdateTimerUI(){
         //set timer UI
        secondsCount += Time.deltaTime;
        timerText.text = hourCount +"h:"+ minuteCount +"m:"+(int)secondsCount + "s";
        if(secondsCount >= 60){
            minuteCount++;
            secondsCount = 0;
        }else if(minuteCount >= 60){
            hourCount++;
            minuteCount = 0;
        }    
    }
    private IEnumerator gameEnd(){
        endGame.SetActive(true);
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }   
}
