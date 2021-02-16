using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Missile : MonoBehaviour
{
    public Vector3 startPos;
    public Vector3 endPos;
    private Vector3 center;
    private float radius;

    private void Awake()
    {
        transform.position = new Vector3(0, 0, 0);
        startPos = transform.position;
        endPos = new Vector3(10, 0, 0);
        radius = Vector3.Distance(startPos, endPos) / 2;
        center = (startPos + endPos) / 2 + new Vector3(0, -0.02f, 0);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update() {
        Vector3 riseRelCenter = startPos - center;
        Vector3 setRelCenter = endPos - center;
        transform.position = Vector3.Slerp(riseRelCenter, setRelCenter, Time.time * 0.1f) + (startPos + endPos) / 2;
        //transform.position += center;
        
        //transform.rotation = Quaternion.LookRotation((startPos + endPos) / 2);
        transform.LookAt(center, center);
    }
}
