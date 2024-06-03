using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Overworld_Player : Overworld_Character
{
    [SerializeField] public Player data;



    // Start is called before the first frame update
    void Start()
    {
        initialize();
    }

    // Update is called once per frame
    void Update()
    {
        calculatePosition();
        processMovement();
    }

    public override Character getData()
    {
        return data;
    }
}
