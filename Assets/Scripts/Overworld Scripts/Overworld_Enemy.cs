using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Overworld_Enemy : Overworld_Character
{

    [SerializeField] public Enemy data;
    private bool hasMovedAlready = false;

    // Start is called before the first frame update
    void Start()
    {
        initialize();
    }

    // Update is called once per frame
    void Update()
    {
        if (isCurrentPlayer && !hasMovedAlready) {
            StartCoroutine(startMovementCoroutine());
        }
        
        calculatePosition();
    }

    private IEnumerator startMovementCoroutine() {
        hasMovedAlready = true;

        yield return new WaitForSeconds(1);

        calculatePosition();
        processMovement();
    }

    override protected void processMovement() {

        var neighbors = currentTile.getNeighbors();
        //this is a very goofy solution
        int randomVal = (int) Random.Range(0.0f, neighbors.Count - 0.01f);

        bool couldMove = moveToTile(neighbors[randomVal]);
        if (!couldMove)
            throw new System.Exception("Random movement picked a non-moveable tile");

        turnManager.nextTurn();
        hasMovedAlready = false;

    }

    public override Character getData()
    {
        return data;
    }

}
