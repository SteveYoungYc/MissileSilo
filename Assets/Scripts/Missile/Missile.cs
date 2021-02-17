﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Missile : MonoBehaviour
{
    
    private List<Vector3> pointsList;

    public int state;

    private float circleTime;
    private bool circleFlag;
    private Vector3 worldUp;

    private float verticalHeight;
    private float speed;
    private readonly float maxSpeed = 7;

    private void Awake()
    {
        //transform.position = new Vector3(0, 0, 0);
        pointsList = new List<Vector3>();
        state = 0;
        circleFlag = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        speed = maxSpeed;
        verticalHeight = 10f;
        SetTarget(new Vector3(0, 0, 0));
    }

    // Update is called once per frame
    void Update()
    {
        Fly();
    }

    private void Fly()
    {
        StateSwitch();
        switch (state)
        {
            case -1: break;
            case 0: LinearOrbit(pointsList[0], pointsList[1]); break;
            case 1: CircleOrbit(pointsList[1], pointsList[2]); break;
            case 2: LinearOrbit(pointsList[2], pointsList[3]); break;
        }
    }

    private void CircleOrbit(Vector3 start, Vector3 end)
    {
        Vector3 center = (start + end) / 2;
        float radius = Vector3.Distance(center, start);
        float fai = Mathf.Atan((center.z - start.z) / (center.x - start.x));
        print(fai / Mathf.PI * 180f);
        float x = center.x - radius * Mathf.Cos((Time.time - circleTime) * (speed / radius * 8)) * Mathf.Cos(fai);
        float y = center.y + radius * Mathf.Sin((Time.time - circleTime) * (speed / radius * 8));
        float z = center.z - radius * Mathf.Cos((Time.time - circleTime) * (speed / radius * 8)) * Mathf.Sin(fai);
        transform.position = new Vector3(x, y, z);
        transform.LookAt(center, worldUp);
    }

    private void LinearOrbit(Vector3 start, Vector3 end)
    {
        transform.Translate((end - start) * (Time.deltaTime * speed), Space.World);
    }

    private void StateSwitch()
    {
        //print(transform.position);
        if (Vector3.Distance(transform.position, pointsList[0]) < 0.1)
        {
            state = 0;
        }
        
        if (Vector3.Distance(transform.position, pointsList[1]) < 0.5)
        {
            state = 1;
            if(circleFlag)
                circleTime = Time.time;
            circleFlag = false;
        }
        else if (Vector3.Distance(transform.position, pointsList[1]) < 1)
        {
            //speed = 1;
        }
        else
        {
            //speed = maxSpeed;
        }
        
        if (Vector3.Distance(transform.position, pointsList[2]) < 0.5)
        {
            state = 2;
        }
        if (Vector3.Distance(transform.position, pointsList[3]) < 0.5)
        {
            state = -1;
            Destroy(gameObject);
        }
    }

    void SetTarget(Vector3 targetPos)
    {
        var position = transform.position;
        if (targetPos.y - position.y > verticalHeight)
        {
            Debug.LogError("The target is too high.");
        }
        pointsList = new List<Vector3>
        {
            position,
            new Vector3(position.x, position.y + verticalHeight, position.z),
            new Vector3(targetPos.x, position.y + verticalHeight, targetPos.z),
            targetPos
        };
        worldUp = Vector3.Normalize(new Vector3(pointsList[3].x - pointsList[0].x, 0, pointsList[3].z - pointsList[0].z));
    }
}
