using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaggyFollow : MonoBehaviour
{
    public float followSpeed;
    public GameObject followObject;
    private Vector3 targetPos;
    private Quaternion targetRot;
    public Vector3 positionOffset;

    // Start is called before the first frame update
    void Start()
    {
        targetPos = followObject.transform.position;
        targetRot = followObject.transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        targetPos = Vector3.Lerp(targetPos, followObject.transform.position, Time.deltaTime * followSpeed);
        targetRot = Quaternion.Lerp(targetRot, followObject.transform.rotation, Time.deltaTime * followSpeed);

        this.transform.position = targetPos + positionOffset;
        this.transform.rotation = targetRot;
    }
}
