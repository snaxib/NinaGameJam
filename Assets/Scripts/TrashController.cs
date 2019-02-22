using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject gameController;
    Renderer rend;
    MeshFilter model;
    private int fill = 0;
    private bool multi = true;
    private List<Transform> trashCans = new List<Transform>();
    private GameObject empty;
    private GameObject partial;
    private GameObject full;
    void Start()
    {
        GetChildren();
        partial.SetActive(false);
        full.SetActive(false);
    }

    void LateUpdate() {
        if (fill == 1 && !IsFull()){
          empty.SetActive(false);
          partial.SetActive(true);
        }
        else if (IsFull()){
          partial.SetActive(false);
          full.SetActive(true);          
        }
    }
    void OnTriggerEnter(Collider other){
        if (other.gameObject.CompareTag ("Can")){
            if (!IsFull()){
                Debug.Log("Score!");
                gameController.SendMessage("IncrementScore");
                //Debug.Log(score);
                Destroy(other.gameObject);
                gameController.SendMessage("SpawnCan", multi);
                fill += 1;
            }
        }
    }
    bool IsFull(){
        if (fill >= 3){
            return true;
        }
        else{
            return false;
        }
    }
    void GetChildren(){
        empty = GameObject.FindGameObjectWithTag("Empty");
        partial = GameObject.FindGameObjectWithTag("Partial");
        full = GameObject.FindGameObjectWithTag("Full");

    }

}
/*     public static class GameObjectExtensions
 {
    public static GameObject GetChild(this GameObject inside, string wanted, bool recursive = false)
    {
        foreach (Transform child in inside.transform)
        {
            if (child.name == wanted) return child.gameObject;
            if (recursive)
            {
                var within = GetChild(child.gameObject, wanted, true);
                if (within) return within;
            }
        }
        return null;
    }
 }*/