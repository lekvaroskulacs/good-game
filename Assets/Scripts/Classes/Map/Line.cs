using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Line : MonoBehaviour
{
    [SerializeField] private Tile first;
    [SerializeField] private Tile second;
    public Tile First {
        get {return first;} 
        set {first = value;}
    }

    public Tile Second {
        get {return second;}
        set {second = value;}
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        calculateLinePosition();
    }

    public void calculateLinePosition() {

        if (First == null || Second == null) return;    

        Vector3 firstPos = First.transform.position;
        Vector3 secondPos = Second.transform.position;

        //position
        Vector3 middle = (firstPos + secondPos) / 2;
        transform.position = middle;

        //scale
        float dist = Vector3.Distance(firstPos, secondPos);
        transform.localScale = new Vector3(dist, transform.localScale.y);

        //rotation
        Vector3 xIdentity = new Vector3(1.0f, 0, 0);
        Vector3 firstToSecond = secondPos - firstPos;
        float angle = Vector3.Angle(xIdentity, firstToSecond);
        if (firstToSecond.y <= 0)
            angle = -angle;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

    }
}
