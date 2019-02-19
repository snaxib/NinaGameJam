using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour
{
    private int score;
    public Text scoreText;
    // Start is called before the first frame update
    void Start()
    {

        score = 0;  
        SetScoreText();
    }

    // Update is called once per frame

    void OnTriggerEnter(Collider other){
        if (other.gameObject.CompareTag ("Can")){
            Debug.Log("something Hit me");
            Debug.Log("Score!");
            
            score += 1;
            SetScoreText();
            //Debug.Log(score);
            Destroy(other.gameObject);
     }
    }

    void SetScoreText(){
        scoreText.text = "Score: " + score.ToString();
    }

}
