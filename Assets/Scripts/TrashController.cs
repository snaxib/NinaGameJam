﻿using System.Collections;
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
    private ParticleSystem confettiSystem;
    private FullIndicatorController fullIndicatorScript;

    void Start()
    {
        GetChildren();
        partial.SetActive(false);
        full.SetActive(false);

        confettiSystem = this.GetComponentInChildren<ParticleSystem>();
        fullIndicatorScript = this.GetComponentInChildren<FullIndicatorController>();
    }

    void LateUpdate() {
        if (fill == 2 && !IsFull()){
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
                gameController.SendMessage("IncrementScore");
                //Debug.Log(score);
                Destroy(other.gameObject);
                gameController.SendMessage("SpawnCan", multi);
                fill += 1;
                FireConfetti();
                FillIndicator();
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
        foreach(Transform child in transform){
            if(child.gameObject.tag == "Empty"){
                empty = child.gameObject;
            }
          if(child.gameObject.tag == "Partial"){
                partial = child.gameObject;
            }
            if(child.gameObject.tag == "Full"){
                full = child.gameObject;
            }
        }
    }

    private void FireConfetti()
    {
        if (confettiSystem != null)
        {
            confettiSystem.Play();
        }
    }

    private void FillIndicator()
    {
        if (fullIndicatorScript != null)
        {
            fullIndicatorScript.SetCanCount(fill);
        }
    }

}
