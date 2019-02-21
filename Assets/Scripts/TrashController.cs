using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject gameController;
    void Start()
    {
        
    }

    // Update is called once per frame
 void OnTriggerEnter(Collider other){
        if (other.gameObject.CompareTag ("Can")){
            Debug.Log("Score!");
            gameController.SendMessage("IncrementScore");
            //Debug.Log(score);
            Destroy(other.gameObject);
            gameController.SendMessage("SpawnCan");
     }
    }
}
