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

    private List<Vector3> pointsList;

    public int state;

    private void Awake()
    {
        //transform.position = new Vector3(0, 0, 0);
        startPos = transform.position;
        endPos = new Vector3(10, 0, 0);
        pointsList = new List<Vector3>
        {
            new Vector3(0, 0, 0), new Vector3(0, 10, 0), new Vector3(10, 10, 0), new Vector3(10, 0, 0)
        };

        transform.position = pointsList[0];
        state = 0;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //CircleOrbit(pointsList[1],pointsList[2]);
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
        Vector3 center = (start + end) / 2 + new Vector3(0, -0.2f, 0);
        transform.position = Vector3.Slerp(start - center, end - center, Time.time / 10) + center;//(start + end) / 2
        transform.LookAt(center, Vector3.right);
        print(transform.position);
    }

    private void LinearOrbit(Vector3 start, Vector3 end)
    {
        transform.Translate((end - start) * Time.deltaTime / 3, Space.World);
    }

    private void StateSwitch()
    {
        if (Vector3.Distance(transform.position, pointsList[0]) < 0.01)
        {
            state = 0;
        }
        if (Math.Abs(transform.position.y - pointsList[1].y) < 0.01) //Vector3.Distance(transform.position, pointsList[1]) < 0.01
        //transform.position.y - pointsList[1].y > 0.1 && transform.position.y - pointsList[1].y < 0.2
        {
            state = 1;
            print(transform.position);
        }
        if (Vector3.Distance(transform.position, pointsList[2]) < 0.03)
        {
            state = 2;
        }
        if (Vector3.Distance(transform.position, pointsList[3]) < 1)
        {
            state = -1;
        }
    }
}
