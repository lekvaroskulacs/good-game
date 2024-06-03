using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;

abstract public class Overworld_Character : MonoBehaviour
{

    [SerializeField] public Tile currentTile;
    [SerializeField] public bool isCurrentPlayer = false;
    protected bool movedThisFrame = false;
    protected TurnManager turnManager;

    protected void initialize() {
        turnManager = GameObject.FindGameObjectWithTag("TurnManager").GetComponent<TurnManager>();
    }

    protected void calculatePosition() {

        if (currentTile == null) return;

        transform.position = currentTile.transform.position;

    }

    public bool moveToTile(Tile newTile) {

        if (newTile.canMoveHere(currentTile)) {
            currentTile = newTile;
            return true;
        }
        else {
            Debug.Log("Uh oh, thats illegal!"); 
            return false;
        }

    }

    protected virtual void processMovement() {
        if (!isCurrentPlayer) return;

        Tile clicked = Tile.getClickedTile();
        if (clicked && Input.GetMouseButtonDown(1)) {

            bool couldMove = moveToTile(clicked);
            
            if (couldMove) turnManager.nextTurn();

        }
    }

    public abstract Character getData();

    


}
