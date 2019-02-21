using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject gameController;
    private int fill = 0;
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate() {

    }
    void OnTriggerEnter(Collider other){
        if (other.gameObject.CompareTag ("Can")){
            if (!IsFull()){
                Debug.Log("Score!");
                gameController.SendMessage("IncrementScore");
                //Debug.Log(score);
                Destroy(other.gameObject);
                gameController.SendMessage("SpawnCan", false);
                fill += 1;
            }
        }
    }
    bool IsFull(){
        if (fill >= 4){
            return true;
        }
        else{
            return false;
        }
    }
}
